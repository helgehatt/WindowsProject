using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class MoveShape : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ClassShape element;
        private Point start;
        private Point end;
        private double offsetX = 0.0;
        private double offsetY = 0.0;

        public MoveShape(MouseButtonEventArgs e)
        {
            this.element = (ClassShape)e.Source;
            //Setup move command.
            StartMoveShape(e);
            //Create Event Handlers to perform move command.
            MouseEventHandler mousemove = new MouseEventHandler(ProgressMoveShape);
            MouseButtonEventHandler mouseup = null;
            mouseup = new MouseButtonEventHandler((object sender, MouseButtonEventArgs e1) => { EndMoveShape(sender, e1); element.MouseMove -= mousemove; element.MouseLeftButtonUp -= mouseup; });
            //Add these to the element to be moved.
            element.MouseMove += mousemove;
            element.MouseLeftButtonUp += mouseup;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute() => Execute(null);
        public void Execute(object parameter)
        {
            // Moves the element.
            element.SetValue(Canvas.LeftProperty, end.X);
            element.SetValue(Canvas.TopProperty, end.Y);
        }

        private void StartMoveShape(MouseButtonEventArgs e)
        {
            element.Cursor = Cursors.Hand;
            Mouse.Capture((IInputElement)element);
            //Get current position.
            start = new Point(Canvas.GetLeft(element), Canvas.GetTop(element));
            end = start;
            //Calculate cursor offset from element origin.
            var cursorPos = e.GetPosition(element.Parent as Canvas);
            this.offsetX = start.X - cursorPos.X;
            this.offsetY = start.Y - cursorPos.Y;
            e.Handled = true;
        }

        private void EndMoveShape(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            element.Cursor = Cursors.Arrow;
            e.Handled = true;
        }

        private void ProgressMoveShape(object sender, MouseEventArgs e)
        {
            //Get new position.
            var moveTo = e.GetPosition(element.Parent as Canvas);
            moveTo.Offset(offsetX, offsetY);
            element.SetValue(Canvas.TopProperty, moveTo.Y);
            element.SetValue(Canvas.LeftProperty, moveTo.X);
            end = new Point(moveTo.X, moveTo.Y);
            e.Handled = true;
        }
    }
}
