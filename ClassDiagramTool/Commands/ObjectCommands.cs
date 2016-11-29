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

        private ConnectionPointViewModel From;

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

            new ConnectionPointViewModel(EConnectionPoint.North, ShapeViewModel);
            new ConnectionPointViewModel(EConnectionPoint.South, ShapeViewModel);
            new ConnectionPointViewModel(EConnectionPoint.East , ShapeViewModel);
            new ConnectionPointViewModel(EConnectionPoint.West , ShapeViewModel);

            UndoRedoController.Execute(new AddShapeCommand(ShapeViewModels, new List<ShapeViewModel>() { ShapeViewModel }));
        }

        public void DeleteShapes()
        {
            List<ShapeViewModel> SelectedShapeViewModels = SelectedObjectsController.SelectionList.Select(o => o.DataContext as ShapeViewModel).ToList();

            SelectedObjectsController.DeselectAll();

            UndoRedoController.Execute(new DeleteShapeCommand(ShapeViewModels, LineViewModels, SelectedShapeViewModels));
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

        public void AddLine(MouseButtonEventArgs e)
        {
            var UserControl = e.Source as UserControl;
            
            if (UserControl == null) { Debug.WriteLine("AddLine, UserControl == null"); return; }

            var ConnectionPointViewModel = UserControl.DataContext as ConnectionPointViewModel;

            if (ConnectionPointViewModel == null) Debug.WriteLine("AddLine, ConnectionPointViewModel == null");
            else if (From == null) From = ConnectionPointViewModel;
            else if (From.ShapeViewModel != ConnectionPointViewModel.ShapeViewModel)
            {
                LineViewModel LineViewModel = null;

                switch (SelectedLine)
                {
                    case ELine.Aggregation          : LineViewModel = new AggregationViewModel           (From, ConnectionPointViewModel);   break;
                    case ELine.Association          : LineViewModel = new AssociationViewModel           (From, ConnectionPointViewModel);   break;
                    case ELine.Composition          : LineViewModel = new CompositionViewModel           (From, ConnectionPointViewModel);   break;
                    case ELine.Dependency           : LineViewModel = new DependencyViewModel            (From, ConnectionPointViewModel);   break;
                    case ELine.DirectedAssociation  : LineViewModel = new DirectedAssociationViewModel   (From, ConnectionPointViewModel);   break;
                    case ELine.Inheritance          : LineViewModel = new InheritanceViewModel           (From, ConnectionPointViewModel);   break;
                    case ELine.InterfaceRealization : LineViewModel = new InterfaceRealizationViewModel  (From, ConnectionPointViewModel);   break;
                }

                if (LineViewModel == null) { Debug.WriteLine("AddLine, LineViewModel == null"); return; }

                UndoRedoController.Execute(new AddLineCommand(LineViewModels, LineViewModel));

                From = null;
            }
        }

        public void AddConnectionPoint(MouseButtonEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift)) return;

            var ShapeControl = e.Source as ShapeControl;

            if (ShapeControl == null) { Debug.WriteLine("AddConnectionPoint, ShapeControl == null"); return; }

            var ShapeViewModel = ShapeControl.DataContext as ShapeViewModel;

            if (ShapeViewModel == null) { Debug.WriteLine("AddConnectionPoint, ShapeViewModel == null"); return; }

            Point MousePos = e.GetPosition(ShapeControl);

            ConnectionPointViewModel ConnectionPointViewModel = null;

                 if (MousePos.Y <= 5) ConnectionPointViewModel = new ConnectionPointViewModel(EConnectionPoint.North, ShapeViewModel);
            else if (MousePos.X <= 5) ConnectionPointViewModel = new ConnectionPointViewModel(EConnectionPoint.West , ShapeViewModel);
            else if (ShapeViewModel.Height - MousePos.Y <= 5) ConnectionPointViewModel = new ConnectionPointViewModel(EConnectionPoint.South, ShapeViewModel);
            else if (ShapeViewModel.Width  - MousePos.X <= 5) ConnectionPointViewModel = new ConnectionPointViewModel(EConnectionPoint.East , ShapeViewModel);

            if (ConnectionPointViewModel == null) { Debug.WriteLine("AddConnectionPoint, ConnectionPointViewModel == null"); return; }

            switch(ConnectionPointViewModel.Orientation)
            {
                case EConnectionPoint.North:
                case EConnectionPoint.South:
                    ConnectionPointViewModel.Percentile = MousePos.X / ShapeViewModel.Width;
                    break;
                case EConnectionPoint.East :
                case EConnectionPoint.West:
                    ConnectionPointViewModel.Percentile = MousePos.Y / ShapeViewModel.Height;
                    break;
            }

            UndoRedoController.Execute(new AddConnectionPointCommand(ShapeViewModel.ConnectionPointViewModels, ConnectionPointViewModel));
        }

        public void StartAddingLine()
        {
            if (MainViewModel.IsAddingLine) return;

            MainViewModel.IsAddingLine = true;

            foreach (var ShapeViewModel in ShapeViewModels)
                foreach (var ConnectionPointViewModel in ShapeViewModel.ConnectionPointViewModels)
                    ConnectionPointViewModel.NotifyVisibility();

            SelectedObjectsController.DeselectAll();

            // Blur canvas
        }

        public void StopAddingLine()
        {
            if (!MainViewModel.IsAddingLine) return;

            MainViewModel.IsAddingLine = false;
            From = null;

            foreach (var ShapeViewModel in ShapeViewModels)
                foreach (var ConnectionPointViewModel in ShapeViewModel.ConnectionPointViewModels)
                    ConnectionPointViewModel.NotifyVisibility();

        }
    }
}
