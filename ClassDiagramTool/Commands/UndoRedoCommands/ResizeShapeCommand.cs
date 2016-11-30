using ClassDiagramTool.ViewModel;
using ClassDiagramTool.Tools;

namespace ClassDiagramTool.Commands.UndoRedoCommands
{
    class ResizeShapeCommand : IUndoRedoCommand
    {
        private ShapeViewModel Shape;
        private double OriginalX, OriginalY, OriginalWidth, OriginalHeight;
        private double NewX, NewY, NewWidth, NewHeight;

        public ResizeShapeCommand(ShapeViewModel shapeViewModel)
        {
            Shape = shapeViewModel;
            OriginalX = shapeViewModel.X;
            OriginalY = shapeViewModel.Y;
            OriginalWidth = shapeViewModel.Width;
            OriginalHeight = shapeViewModel.Height;
        }

        internal void FinalizeResize()
        {
            NewX = Shape.X;
            NewY = Shape.Y;
            NewWidth = Shape.Width;
            NewHeight = Shape.Height;
            if (OriginalWidth != NewWidth || OriginalHeight != NewHeight)
                UndoRedoController.Instance.Execute(this);
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
