using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.View.ShapeControls;
using ClassDiagramTool.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class ObjectCommands
    {
        private MainViewModel MainViewModel;

        private SelectedObjectsController SelectedObjectsController => SelectedObjectsController.Instance;
        private UndoRedoController        UndoRedoController        => UndoRedoController.Instance;

        private ShapeViewModel From;
        private int FromPoint;

        private EShape SelectedShape => MainViewModel.SelectedShape;
        private ELine  SelectedLine  => MainViewModel.SelectedLine;

        private ObservableCollection<ShapeViewModel> ShapeViewModels => MainViewModel.ShapeViewModels;
        private ObservableCollection<LineViewModel>  LineViewModels  => MainViewModel.LineViewModels;

        public ObjectCommands(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public void AddShapes(MouseButtonEventArgs e)
        {
            Canvas Canvas = e.Source as Canvas;

            Point MousePos = Mouse.GetPosition(Canvas);

            ShapeViewModel ShapeViewModel = null;

            switch (SelectedShape)
            {
                case EShape.Class       : ShapeViewModel = new ClassViewModel        () { CenterX = MousePos.X, CenterY = MousePos.Y };  break;
                case EShape.Enumeration : ShapeViewModel = new EnumerationViewModel  () { CenterX = MousePos.X, CenterY = MousePos.Y };  break;
                case EShape.Interface   : ShapeViewModel = new InterfaceViewModel    () { CenterX = MousePos.X, CenterY = MousePos.Y };  break;
            }

            if (ShapeViewModel == null) { Debug.WriteLine("OnAddShapeCommand, ShapeViewModel == null"); return; }

            UndoRedoController.Execute(new AddShapeCommand(ShapeViewModels, new List<ShapeViewModel>() { ShapeViewModel }));
        }

        public void DeleteShapes()
        {
            List<ShapeViewModel> SelectedShapeViewModels = SelectedObjectsController.SelectionList.Select(o => o.DataContext as ShapeViewModel).ToList();
            UndoRedoController.Execute(new DeleteShapeCommand(ShapeViewModels, SelectedShapeViewModels));
        }

        public void SelectShape(MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (SelectedObjectsController.IsSelected(e.Source as ShapeControl))
                    SelectedObjectsController.Deselect(e.Source as ShapeControl);
                else
                    SelectedObjectsController.AddSelect(e.Source as ShapeControl);
            }
            else
            {
                SelectedObjectsController.Select(e.Source as ShapeControl);
            }
        }

        public void AddLine(ShapeViewModel shapeViewModel, int shapePoint)
        {
            //ShapeControl ShapeControl = e.Source as ShapeControl;
            //
            //if (ShapeControl == null) return;
            //
            //ShapeViewModel shapeViewModel = ShapeControl.DataContext as ShapeViewModel;

            if (shapeViewModel == null) Debug.WriteLine("OnAddLineCommand, ShapeViewModel == null");
            else if (From == null) { From = shapeViewModel; FromPoint = shapePoint; }
            else if (From != shapeViewModel)
            {
                LineViewModel LineViewModel = null;

                switch (SelectedLine)
                {
                    case ELine.Aggregation          : LineViewModel = new AggregationViewModel           (From, FromPoint, shapeViewModel, shapePoint);   break;
                    case ELine.Association          : LineViewModel = new AssociationViewModel           (From, FromPoint, shapeViewModel, shapePoint);   break;
                    case ELine.Composition          : LineViewModel = new CompositionViewModel           (From, FromPoint, shapeViewModel, shapePoint);   break;
                    case ELine.Dependency           : LineViewModel = new DependencyViewModel            (From, FromPoint, shapeViewModel, shapePoint);   break;
                    case ELine.DirectedAssociation  : LineViewModel = new DirectedAssociationViewModel   (From, FromPoint, shapeViewModel, shapePoint);   break;
                    case ELine.Inheritance          : LineViewModel = new InheritanceViewModel           (From, FromPoint, shapeViewModel, shapePoint);   break;
                    case ELine.InterfaceRealization : LineViewModel = new InterfaceRealizationViewModel  (From, FromPoint, shapeViewModel, shapePoint);   break;
                }

                if (LineViewModel == null) { Debug.WriteLine("OnAddLineCommand, LineViewModel == null"); return; }

                UndoRedoController.Execute(new AddLineCommand(LineViewModels, LineViewModel));

                From = null;
            }
        }

        public void AddingLine()
        {
            MainViewModel.IsAddingLine = !MainViewModel.IsAddingLine;
            // Add ConnectionPointAdorners
            // Blur canvas
            // Disable on esc, right-click, etc
        }
    }
}
