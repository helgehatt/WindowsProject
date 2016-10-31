using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MyMouseClickEvent(e.Source, e);
        }
        public void MyMouseClickEvent(object sender, MouseButtonEventArgs e)
        {

            Point position = Mouse.GetPosition(sender as Canvas);

            MyShape rectangle = new MyShape();

            Canvas.SetTop(rectangle, position.Y);
            Canvas.SetLeft(rectangle, position.X);
            (sender as Canvas).Children.Add(rectangle);
        }
    }
}
