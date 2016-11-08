using ClassDiagramTool.ViewModel;
using System.Collections.ObjectModel;

namespace ClassDiagramTool.Commands
{
    class DeleteObjectCommand
    {
        private ObservableCollection<BaseViewModel> Objects;
        private BaseViewModel Object;

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
    }
}
