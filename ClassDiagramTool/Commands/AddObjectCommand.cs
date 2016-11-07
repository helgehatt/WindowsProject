using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class AddObjectCommand : IUndoRedoCommand
    {
        private ObservableCollection<BaseViewModel> Objects;
        private BaseViewModel Object;

        public AddObjectCommand(ObservableCollection<BaseViewModel> objects, BaseViewModel @object)
        {
            Objects = objects;
            Object = @object;
        }

        public void Execute()
        {
            Objects.Add(Object);
        }

        public void UnExecute()
        {
            Objects.Remove(Object);
        }
    }
}
