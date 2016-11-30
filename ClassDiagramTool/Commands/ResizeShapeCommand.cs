using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.Commands
{
    class ResizeShapeCommand : IUndoRedoCommand
    {
        ShapeViewModel Shape;
        double OriginalX, OriginalY, OriginalWidth, OriginalHeight;
        double NewX, NewY, NewWidth, NewHeight;

        public ResizeShapeCommand(ShapeViewModel shape)
        {
            Shape = shape;
            OriginalX = shape.X;
            OriginalY = shape.Y;
            OriginalWidth = shape.Width;
            OriginalHeight = shape.Height;
        }

        public void FinalizeResize()
        {
            NewX = Shape.X;
            NewY = Shape.Y;
            NewWidth = Shape.Width;
            NewHeight = Shape.Height;
            if (OriginalX != NewX || OriginalY != NewY || OriginalWidth != NewWidth || OriginalHeight != NewHeight)
                UndoRedoController.Instance.AddAndExecute(this);
        }

        public void Execute()
        {
            Shape.X = NewX;
            Shape.Y = NewY;
            Shape.Width = NewWidth;
            Shape.Height = NewHeight;
        }

        public void UnExecute()
        {
            Shape.X = OriginalX;
            Shape.Y = OriginalY;
            Shape.Width = OriginalWidth;
            Shape.Height = OriginalHeight;
        }
    }
}
