using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClassDiagramTool.Commands
{
    class ClipboardCommands
    {
        private MainViewModel MainViewModel;

        private SelectedObjectsController SelectedObjectsController => SelectedObjectsController.Instance;
        private UndoRedoController        UndoRedoController        => UndoRedoController.Instance;

        private ObservableCollection<ShapeViewModel> ShapeViewModels => MainViewModel.ShapeViewModels;
        private ObservableCollection<LineViewModel>  LineViewModels  => MainViewModel.LineViewModels;

        public ClipboardCommands(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public void Cut()
        {
            var SelectedShapeViewModels = SelectedObjectsController.SelectionList.Select(o => o.DataContext as ShapeViewModel).ToList();
            var Shapes                  = SelectedShapeViewModels.Select(o => o.Shape).ToList();
            var ConnectionPoints        = SelectedShapeViewModels.SelectMany(o => (o.ConnectionPointViewModels.Select(p => p.ConnectionPoint)).ToList()).ToList();

            var Data = new DataObject();
            Data.SetData("Shapes", Shapes);
            Data.SetData("ConnectionPoints", ConnectionPoints);

            Clipboard.Clear();
            Clipboard.SetDataObject(Data);

            SelectedObjectsController.DeselectAll();
            UndoRedoController.Execute(new DeleteCommand(ShapeViewModels, LineViewModels, SelectedShapeViewModels));
        }

        public void Copy()
        {
            var SelectedShapeViewModels   = SelectedObjectsController.SelectionList.Select(o => o.DataContext as ShapeViewModel).ToList();
            var ConnectionPointViewModels = SelectedShapeViewModels.SelectMany(o => o.ConnectionPointViewModels).ToList();
            var LineViewModels            = ConnectionPointViewModels.SelectMany(o => o.LineViewModels).ToList();

            // The LineViewModels connecting the selected shapes will appear as duplicates in the LineViewModels list
            var Lines                     = LineViewModels.GroupBy(o => o).Where(o => o.Count() > 1).Select(o => o.Key).Select(o => o.Line).ToList();
            var ConnectionPoints          = ConnectionPointViewModels.Select(p => p.ConnectionPoint).ToList();
            var Shapes                    = SelectedShapeViewModels.Select(o => o.Shape).ToList();

            var Data = new DataObject();
            Data.SetData("Shapes", Shapes);
            Data.SetData("ConnectionPoints", ConnectionPoints);
            Data.SetData("Lines", Lines);

            Clipboard.Clear();
            Clipboard.SetDataObject(Data);
        }

        public void Paste()
        {
            SelectedObjectsController.DeselectAll();

            var Data = Clipboard.GetDataObject() as DataObject;

            var Shapes           = Data.GetData("Shapes") as List<Shape>;
            var ConnectionPoints = Data.GetData("ConnectionPoints") as List<ConnectionPoint>;
            var Lines            = Data.GetData("Lines") as List<Line>;

            foreach (var Shape in Shapes)
            {
                int NewNumber = Shape.number++;

                foreach (var ConnectionPoint in ConnectionPoints)
                {
                    if (ConnectionPoint.OnShape == Shape.Number)
                        ConnectionPoint.OnShape = NewNumber;
                }

                if (Lines != null) foreach (var Line in Lines)
                {
                    if (Line.FromShape == Shape.Number)
                        Line.FromShape = NewNumber;
                    else
                    if (Line.ToShape == Shape.Number)
                        Line.ToShape = NewNumber;
                }

                Shape.Number = NewNumber;
                Shape.X += 10;
                Shape.Y += 10;
            }

            if (Lines != null)
            {
                foreach (var ConnectionPoint in ConnectionPoints)
                {
                    int NewNumber = ConnectionPoint.number++;

                    foreach (var Line in Lines)
                    {
                        if (Line.FromPoint == ConnectionPoint.Number)
                            Line.FromPoint = NewNumber;
                        else 
                        if (Line.ToPoint == ConnectionPoint.Number)
                            Line.ToPoint = NewNumber;
                    }

                    ConnectionPoint.Number = NewNumber;
                }
            }
            else
            {
                foreach (var ConnectionPoint in ConnectionPoints)
                {
                    ConnectionPoint.Number = ConnectionPoint.number++;
                }
            }

            var AddedShapeViewModels = MainViewModel.ReconstructShapes(Shapes);

            MainViewModel.ReconstructConnectionPoints(ConnectionPoints, AddedShapeViewModels);

            if (Lines != null)
            {
                var AddedLineViewModels = MainViewModel.ReconstructLines(Lines, AddedShapeViewModels);
                UndoRedoController.Execute(new AddCommand(ShapeViewModels, LineViewModels, AddedShapeViewModels, AddedLineViewModels));
            }
            else
            {
                UndoRedoController.Execute(new AddShapesCommand(ShapeViewModels, AddedShapeViewModels));
            }
        }
    }
}
