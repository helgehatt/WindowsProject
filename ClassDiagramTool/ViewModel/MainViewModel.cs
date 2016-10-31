using ClassDiagramTool.Commands;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClassDiagramTool.ViewModel
{
    class MainViewModel
    {
        public RelayCommand<MouseButtonEventArgs> MyCommand { get; }
        public RelayCommand<MouseButtonEventArgs> helloCommand => new RelayCommand<MouseButtonEventArgs>(Execute, c => true);

        public MainViewModel()
        {


            MyCommand = new RelayCommand<MouseButtonEventArgs>(Execute, c => true);
        }

        private void Execute(MouseButtonEventArgs obj)
        {
            Debug.WriteLine("hellllo");
            var pos = obj.GetPosition(obj.Source as UIElement);
            (new ClickAddShape()).Execute(obj);
        }
    }

    
}
