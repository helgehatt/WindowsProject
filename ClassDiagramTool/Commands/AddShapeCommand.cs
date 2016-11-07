using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class AddShapeCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> Shapes;
        private ShapeViewModel Shape;

        public AddShapeCommand(ObservableCollection<ShapeViewModel> shapes, ShapeViewModel shape)
        {
            Shapes = shapes;
            Shape = shape;
        }

        public void Execute()
        {
            Shapes.Add(Shape);
        }

        public void UnExecute()
        {
            Shapes.Remove(Shape);
        }
    }
}
