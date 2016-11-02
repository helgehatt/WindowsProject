using ClassDiagramTool.UndoRedo;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class MoveShape : IUndoRedoCommand
    {
        public event EventHandler CanExecuteChanged;

        private ClassShape MovedElement;
        private Point OriginalPosition;
        private Point FinalPosition;
        private Point CursorOffset;

        public MoveShape(MouseButtonEventArgs e)
        {
            this.MovedElement = (ClassShape)e.Source;
            //Setup move command.
            SetupMoveShape(e);

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
            MovedElement.SetValue(Canvas.LeftProperty, FinalPosition.X);
            MovedElement.SetValue(Canvas.TopProperty, FinalPosition.Y);
        }

        public void UnExecute()
        {
            MovedElement.SetValue(Canvas.LeftProperty, OriginalPosition.X);
            MovedElement.SetValue(Canvas.TopProperty, OriginalPosition.Y);
        }

        private void SetupMoveShape(MouseButtonEventArgs e)
        {
            MovedElement.Cursor = Cursors.Hand;
            Mouse.Capture((IInputElement)MovedElement);
            //Get current position.
            OriginalPosition = new Point(Canvas.GetLeft(MovedElement), Canvas.GetTop(MovedElement));
            FinalPosition = OriginalPosition;
            //Calculate cursor offset from element origin.
            Point CursorPos = e.GetPosition(MovedElement.Parent as Canvas);
            CursorOffset = new Point(OriginalPosition.X - CursorPos.X, OriginalPosition.Y - CursorPos.Y);
            e.Handled = true;
        }

        private void FinalizeMoveShape(MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            MovedElement.Cursor = Cursors.Arrow;
            e.Handled = true;
        }

        private void UpdateMoveShape(MouseEventArgs e)
        {
            //Get new position.
            Point MoveToPosition = e.GetPosition(MovedElement.Parent as Canvas);
            MoveToPosition.Offset(CursorOffset.X, CursorOffset.Y);
            MovedElement.SetValue(Canvas.TopProperty, MoveToPosition.Y);
            MovedElement.SetValue(Canvas.LeftProperty, MoveToPosition.X);
            FinalPosition = new Point(MoveToPosition.X, MoveToPosition.Y);
            e.Handled = true;
        }
    }
}
