using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class ClickAddShape : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MouseButtonEventArgs e = parameter as MouseButtonEventArgs;
            Canvas mainCanvas = e.Source as Canvas;

            Point position = Mouse.GetPosition(mainCanvas);

            ClassShape newShape = new ClassShape();

            Canvas.SetTop(newShape, position.Y);
            Canvas.SetLeft(newShape, position.X);

            mainCanvas.Children.Add(newShape);
        }
    }
}
