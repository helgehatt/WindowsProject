using ClassDiagramTool.Commands;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace ClassDiagramTool.ViewModel
{
    class MainViewModel
    {
        // MouseClick Add Shape Command
        public RelayCommand<MouseButtonEventArgs> AddShapeCommand => new RelayCommand<MouseButtonEventArgs>(ClickAddShape.Execute, ClickAddShape.CanExecute);
        public ClickAddShape ClickAddShape = new ClickAddShape();

        
    }

    
}
