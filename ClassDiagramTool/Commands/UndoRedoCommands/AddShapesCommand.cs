using ClassDiagramTool.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClassDiagramTool.Commands
{
    class AddShapesCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> ShapeViewModels;
        private List<ShapeViewModel> AddedShapeViewModels;

        public AddShapesCommand(ObservableCollection<ShapeViewModel> shapeViewModels, List<ShapeViewModel> addedShapeViewModels)
        {
            ShapeViewModels = shapeViewModels;
            AddedShapeViewModels = addedShapeViewModels;
        }

        public void Execute()
        {
            foreach (ShapeViewModel ShapeViewModel in AddedShapeViewModels)
                ShapeViewModels.Add(ShapeViewModel);

        }

        public void UnExecute()
        {
            foreach (ShapeViewModel ShapeViewModel in AddedShapeViewModels)
                ShapeViewModels.Remove(ShapeViewModel);
        }
    }
}