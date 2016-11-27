using ClassDiagramTool.Tools;
using ClassDiagramTool.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class MoveShapeCommand : IUndoRedoCommand
    {
        private ShapeViewModel ViewModel;
        private Point OriginalPosition;
        private Point FinalPosition;
        private Point CursorOffset;

        public MoveShapeCommand(ShapeViewModel viewModel, MouseButtonEventArgs e)
        {
            ViewModel = viewModel;
            UserControl MovedElement = e.Source as UserControl;

            //Setup move command.
            SetupMoveShape(MovedElement, e);

            //Construct and add event handlers to moving element.
            MouseEventHandler MouseMove = new MouseEventHandler(
                (object sender, MouseEventArgs e1) => {
                    UpdateMoveShape(e1);
                });
            MouseButtonEventHandler MouseUp = null;
            MouseUp = new MouseButtonEventHandler( //This delegate ends the move command and removes event handlers from element.
                (object sender, MouseButtonEventArgs e2) => {
                    FinalizeMoveShape(e2);
                    MovedElement.MouseMove -= MouseMove;
                    MovedElement.MouseLeftButtonUp -= MouseUp;
                });

            //Add event handlers to the element to be moved.
            MovedElement.MouseMove += MouseMove;
            MovedElement.MouseLeftButtonUp += MouseUp;
        }

        public void Execute()
        {
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
            //element.Cursor = Cursors.Hand;
            ViewModel.Dragging = true;
            Mouse.Capture(element as IInputElement);
            //Get current position.
            OriginalPosition = new Point(ViewModel.X, ViewModel.Y);
            FinalPosition = OriginalPosition;
            //Calculate cursor offset from element origin.
            Point CursorPos = e.GetPosition(element.Parent as Canvas);
            CursorOffset = new Point(OriginalPosition.X - CursorPos.X, OriginalPosition.Y - CursorPos.Y);
        }

        private void FinalizeMoveShape(MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            //((UserControl)e.Source).Cursor = Cursors.Arrow;
            ViewModel.Dragging = false;
            if (!OriginalPosition.Equals(FinalPosition))
                UndoRedoController.Instance.Execute(this);
        }

        private void UpdateMoveShape(MouseEventArgs e)
        {
            //Get new position.
            Point MoveToPosition = e.GetPosition((e.Source as UserControl).Parent as Canvas);
            MoveToPosition.Offset(CursorOffset.X, CursorOffset.Y);
            ViewModel.X = MoveToPosition.X;
            ViewModel.Y = MoveToPosition.Y;
            FinalPosition = new Point(MoveToPosition.X, MoveToPosition.Y);
        }
    }
}
