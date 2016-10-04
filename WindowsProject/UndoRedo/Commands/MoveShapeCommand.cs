using System.Windows;
using AdvancedWPFDemo.Model;
using AdvancedWPFDemo.ViewModel.Shapes;

namespace AdvancedWPFDemo.UndoRedo.Commands
{
    public class MoveShapeCommand : IUndoRedoCommand
    {
        private readonly ShapeViewModel _shape;
        private Point _newPosition;
        private Point _oldPosition;

        public MoveShapeCommand(ShapeViewModel shape, Point oldPostion, Point newPostion)
        {
            _shape = shape;
            _oldPosition = oldPostion;
            _newPosition = newPostion;
        }

        public void Execute()
        {
            _shape.X = _newPosition.X;
            _shape.Y = _newPosition.Y;
        }

        public void UnExecute()
        {
            _shape.X = _oldPosition.X;
            _shape.Y = _oldPosition.Y;
        }
    }
}