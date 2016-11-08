using ClassDiagramTool.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace ClassDiagramTool.Commands
{
    class DeleteObjectCommand : ICommand
    {
        private ObservableCollection<BaseViewModel> Objects;
        private BaseViewModel Object;

        public event EventHandler CanExecuteChanged;

        public DeleteObjectCommand(ObservableCollection<BaseViewModel> objects, BaseViewModel @object)
        {
            Objects = objects;
            Object = @object;
        }

        public void Execute()
        {
            Objects.Remove(Object);
        }

        public void UnExecute()
        {
            Objects.Add(Object);
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
