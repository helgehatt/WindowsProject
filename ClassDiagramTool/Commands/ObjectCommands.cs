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
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class ObjectCommands
    {
        private MainViewModel MainViewModel;

        private SelectedObjectsController SelectedObjectsController => SelectedObjectsController.Instance;
        private UndoRedoController        UndoRedoController        => UndoRedoController.Instance;

        private ShapeViewModel From;

        private EShape SelectedShape => MainViewModel.SelectedShape;
        private ELine  SelectedLine  => MainViewModel.SelectedLine;

        private ObservableCollection<ShapeViewModel> ShapeViewModels => MainViewModel.ShapeViewModels;
        private ObservableCollection<LineViewModel>  LineViewModels  => MainViewModel.LineViewModels;

        public ObjectCommands(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }


        public void AddShape(MouseButtonEventArgs e)
        {
            Canvas MainCanvas = e.Source as Canvas;

            Point Position = Mouse.GetPosition(MainCanvas);

            ShapeViewModel ShapeViewModel = null;

            switch (SelectedShape)
            {
                case EShape.Class       : ShapeViewModel = new ClassViewModel        () { CenterX = Position.X, CenterY = Position.Y };  break;
                case EShape.Enumeration : ShapeViewModel = new EnumerationViewModel  () { CenterX = Position.X, CenterY = Position.Y };  break;
                case EShape.Interface   : ShapeViewModel = new InterfaceViewModel    () { CenterX = Position.X, CenterY = Position.Y };  break;
            }

            if (ShapeViewModel == null) { Debug.WriteLine("OnAddShapeCommand, Shape == null, EShape = " + SelectedShape); return; }

            UndoRedoController.Execute(new AddShapeCommand(ShapeViewModels, new List<ShapeViewModel>() { ShapeViewModel }));
        }

        public void DeleteShape(MouseButtonEventArgs e)
        {
            List<ShapeViewModel> SelectedShapeViewModels = SelectedObjectsController.SelectionList.Select(o => o.DataContext as ShapeViewModel).ToList();
            UndoRedoController.Execute(new DeleteShapeCommand(ShapeViewModels, SelectedShapeViewModels));
        }

        public void SelectShape(MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (SelectedObjectsController.IsSelected(e.Source as UserControl))
                    SelectedObjectsController.Deselect(e.Source as UserControl);
                else
                    SelectedObjectsController.AddSelect(e.Source as UserControl);
            }
            else
                SelectedObjectsController.Select(e.Source as UserControl);
        }

        public void AddLine(MouseButtonEventArgs e)
        {
            UserControl UserControl = e.Source as UserControl;
            if (UserControl == null) return;

            ShapeViewModel ShapeViewModel = UserControl.DataContext as ShapeViewModel;

            if (ShapeViewModel == null) Debug.WriteLine("OnAddLineCommand, DataContext=" + (e.Source as UserControl).DataContext);
            else if (From == null) From = ShapeViewModel;
            else if (From != ShapeViewModel)
            {
                LineViewModel LineViewModel = null;
                switch (SelectedLine)
                {
                    case ELine.Aggregation          : LineViewModel = new AggregationViewModel           (From, ShapeViewModel);   break;
                    case ELine.Association          : LineViewModel = new AssociationViewModel           (From, ShapeViewModel);   break;
                    case ELine.Composition          : LineViewModel = new CompositionViewModel           (From, ShapeViewModel);   break;
                    case ELine.Dependency           : LineViewModel = new DependencyViewModel            (From, ShapeViewModel);   break;
                    case ELine.DirectedAssociation  : LineViewModel = new DirectedAssociationViewModel   (From, ShapeViewModel);   break;
                    case ELine.Inheritance          : LineViewModel = new InheritanceViewModel           (From, ShapeViewModel);   break;
                    case ELine.InterfaceRealization : LineViewModel = new InterfaceRealizationViewModel  (From, ShapeViewModel);   break;
                }
                if (LineViewModel == null) { Debug.WriteLine("OnAddLineCommand, Line == null, ELine = " + SelectedLine); return; }

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
