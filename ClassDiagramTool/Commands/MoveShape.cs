using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.View.UserControls;
using ClassDiagramTool.ViewModel.Shapes;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class MoveShape : IUndoRedoCommand
    {
        private ShapeViewModel ViewModel;
        private Point OriginalPosition;
        private Point FinalPosition;
        private Point CursorOffset;

        public MoveShape(ShapeViewModel viewModel, MouseButtonEventArgs e)
        {
            ViewModel = viewModel;
            UserControl MovedElement = (UserControl)e.Source;

            //Setup move command.
            SetupMoveShape(MovedElement, e);

            //Create Event Handlers to perform move command.
            MouseEventHandler MouseMove = new MouseEventHandler(
                (object sender, MouseEventArgs e1) => { UpdateMoveShape(e1); }
                );
            MouseButtonEventHandler MouseUp = null;
            MouseUp = new MouseButtonEventHandler( //Delegate ends the move command and removes event handlers from element.
                (object sender, MouseButtonEventArgs e2) => { FinalizeMoveShape(e2); MovedElement.MouseMove -= MouseMove; MovedElement.MouseLeftButtonUp -= MouseUp; }
                );

            //Add these to the element to be moved.
            MovedElement.MouseMove += MouseMove;
            MovedElement.MouseLeftButtonUp += MouseUp;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute() => Execute(null);
        public void Execute(object parameter)
        {
            // Moves the element.
            ViewModel.X = FinalPosition.X;
            ViewModel.Y = FinalPosition.Y;
        }

        public void UnExecute()
        {
            ViewModel.X = OriginalPosition.X;
            ViewModel.Y = OriginalPosition.Y;
        }

        private void SetupMoveShape(UserControl element, MouseButtonEventArgs e)
        {
            element.Cursor = Cursors.Hand;
            Mouse.Capture((IInputElement)element);
            //Get current position.
            OriginalPosition = new Point(ViewModel.X, ViewModel.Y);
            FinalPosition = OriginalPosition;
            //Calculate cursor offset from element origin.
            Point CursorPos = e.GetPosition(element.Parent as Canvas);
            CursorOffset = new Point(OriginalPosition.X - CursorPos.X, OriginalPosition.Y - CursorPos.Y);
            e.Handled = true;
        }

        private void FinalizeMoveShape(MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            ((UserControl)e.Source).Cursor = Cursors.Arrow;
            e.Handled = true;
        }

        private void UpdateMoveShape(MouseEventArgs e)
        {
            //Get new position.
            Point MoveToPosition = e.GetPosition((e.Source as UserControl).Parent as Canvas);
            MoveToPosition.Offset(CursorOffset.X, CursorOffset.Y);
            ViewModel.X = MoveToPosition.X;
            ViewModel.Y = MoveToPosition.Y;
            FinalPosition = new Point(MoveToPosition.X, MoveToPosition.Y);
            e.Handled = true;
        }
    }
}
