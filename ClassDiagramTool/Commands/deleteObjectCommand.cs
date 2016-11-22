using ClassDiagramTool.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using ClassDiagramTool.ViewModel.Shapes;
using ClassDiagramTool.UndoRedo;
using System.Collections.Generic;

namespace ClassDiagramTool.Commands
{
    class DeleteObjectCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> Shapes;
        private List<ShapeViewModel> ShapeList;

        public event EventHandler CanExecuteChanged;

        public DeleteObjectCommand(ObservableCollection<ShapeViewModel> shapes, List<ShapeViewModel> shapeList)
        {
            Shapes = shapes;
            ShapeList = shapeList;
        }

        public void Execute()
        {
            foreach (ShapeViewModel shape in ShapeList)
            {
                Shapes.Remove(shape);
            }
        }

        public void UnExecute()
        {
            foreach (ShapeViewModel shape in ShapeList)
            {
                Shapes.Add(shape);
            }
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
