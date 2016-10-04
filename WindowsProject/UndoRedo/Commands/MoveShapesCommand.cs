using System.Collections.Generic;
using AdvancedWPFDemo.ViewModel;
using AdvancedWPFDemo.ViewModel.Shapes;

namespace AdvancedWPFDemo.UndoRedo.Commands
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
