using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class HelleWorld : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Debug.WriteLine(parameter);
        }
    }
}
