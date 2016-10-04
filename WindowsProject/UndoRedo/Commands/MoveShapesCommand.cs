using System.Collections.Generic;
using WindowsProject.ViewModel;
using WindowsProject.ViewModel.Shapes;

namespace WindowsProject.UndoRedo.Commands
{
    public class MoveShapesCommand : IUndoRedoCommand
    {
        private List<ShapeViewModel> shapes;
        
        private double offsetX;
        private double offsetY;
        
        public MoveShapesCommand(List<ShapeViewModel> _shapes, double _offsetX, double _offsetY) 
        {
            shapes = _shapes;
            offsetX = _offsetX;
            offsetY = _offsetY;
        }
        
        public void Execute()
        {
            foreach(var s in shapes)
            {
                s.X = offsetX;
                s.Y = offsetY;
            }
        }
        
        public void UnExecute()
        {
            foreach (var s in shapes)
            {
                s.X = offsetX;
                s.Y = offsetY;
            }
        }
    }
}
