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

        public ObservableCollection<ShapeViewModel> ShapeViewModels => MainViewModel.ShapeViewModels;
        public ObservableCollection<LineViewModel>  LineViewModels  => MainViewModel.LineViewModels;

        public ClipboardCommands(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public void Cut()
        {
            List<ShapeViewModel> SelectedShapeViewModels = SelectedObjectsController.SelectionList.Select(o => o.DataContext as ShapeViewModel).ToList();

            List<Shape> Shapes = SelectedShapeViewModels.Select(o => o.Shape).ToList();

            Clipboard.Clear();
            Clipboard.SetData("Shapes", Shapes);

            SelectedObjectsController.DeselectAll();

            // Remove all lines the selected shapes were connected to
            foreach (var ShapeViewModel in SelectedShapeViewModels)
                foreach (var ConnectionPointViewModel in ShapeViewModel.ConnectionPointViewModels)
                    foreach (var LineViewModel in ConnectionPointViewModel.LineViewModels)
                        LineViewModels.Remove(LineViewModel);

            UndoRedoController.Execute(new DeleteShapeCommand(ShapeViewModels, SelectedShapeViewModels));
        }

        public void Copy()
        {
            List<Shape> Shapes = SelectedObjectsController.SelectionList.Select(o => (o.DataContext as ShapeViewModel).Shape).ToList();

            Clipboard.Clear();
            Clipboard.SetData("Shapes", Shapes);
        }

        public void Paste()
        {           
            List<Shape> Shapes = Clipboard.GetData("Shapes") as List<Shape>;

            foreach (Shape Shape in Shapes)
            {
                ShapeViewModel ShapeViewModel = null;

                switch (Shape.Type)
                {
                    case EShape.Class:          ShapeViewModel = new ClassViewModel      (Shape); break;
                    case EShape.Enumeration:    ShapeViewModel = new EnumerationViewModel(Shape); break;
                    case EShape.Interface:      ShapeViewModel = new InterfaceViewModel  (Shape); break;
                }

                if (ShapeViewModel == null) { Debug.WriteLine("Paste, ShapeViewModel == null"); continue; }

                ShapeViewModels.Add(ShapeViewModel);
            }
        }
    }
}
