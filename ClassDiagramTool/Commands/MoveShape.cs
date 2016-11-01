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

        public MoveShape(UIElement element)
        {
            this.element = (ClassShape)element;
            mousemove = new MouseEventHandler(ProgressMoveShape);
            mouseup = new MouseButtonEventHandler(EndMoveShape);
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
        
        void Test1(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("MouseMove");
        }
        void Test2(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("MouseUp");
        }

        private MouseEventHandler mousemove;
        private MouseButtonEventHandler mouseup;

        public void StartMoveShape(object sender, MouseButtonEventArgs e)
        {
            element.Cursor = Cursors.Hand;
            Mouse.Capture((IInputElement)element);
            start = new Point(Canvas.GetLeft(element), Canvas.GetTop(element));
            var cursorPos = e.GetPosition(element.Parent as Canvas);
            this.offsetX = start.X - cursorPos.X;
            this.offsetY = start.Y - cursorPos.Y;
            e.Handled = true;
            element.MouseMove += mousemove;
            element.MouseLeftButtonUp += mouseup;
        }

        void EndMoveShape(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            element.Cursor = Cursors.Arrow;
            
            e.Handled = true;
            Test2(sender, e);
            element.MouseMove -= mousemove;
            element.MouseLeftButtonUp -= mouseup;
        }

        void ProgressMoveShape(object sender, MouseEventArgs e)
        {
                var moveTo = e.GetPosition(element.Parent as Canvas);
                moveTo.Offset(offsetX, offsetY);
                element.SetValue(Canvas.TopProperty, moveTo.Y);
                element.SetValue(Canvas.LeftProperty, moveTo.X);
                end = new Point(moveTo.X, moveTo.Y);
                e.Handled = true;
            Test1(sender, e);
        }
    }
}
