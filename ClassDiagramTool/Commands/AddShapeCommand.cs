using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class AddShapeCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> Shapes;
        private List<ShapeViewModel> ShapeList;

        public AddShapeCommand(ObservableCollection<ShapeViewModel> shapes, List<ShapeViewModel> shapeList)
        {
            Shapes = shapes;
            ShapeList = shapeList;
        }
        public void Execute()
        {
            foreach(ShapeViewModel shape in ShapeList)
            {
                Shapes.Add(shape);
            }
            
        }

        public void UnExecute()
        {
            foreach (ShapeViewModel shape in ShapeList)
            {
                Shapes.Remove(shape);
            }
        }
    }
}