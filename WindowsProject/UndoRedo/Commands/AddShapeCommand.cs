using System.Collections.ObjectModel;
using WindowsProject.ViewModel;
using WindowsProject.ViewModel.Shapes;

namespace WindowsProject.UndoRedo.Commands
{
    public class AddShapeCommand : IUndoRedoCommand
    {
        private ObservableShapesCollection shapes;
        private ShapeViewModel shape;

        public AddShapeCommand(ObservableShapesCollection _shapes, ShapeViewModel _shape) 
        { 
            shapes = _shapes;
            shape = _shape;
        }

        public void Execute()
        {
            shapes.Add(shape);
        }
        
        public void UnExecute()
        {
            shapes.Remove(shape);
        }
    }
}
