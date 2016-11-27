using ClassDiagramTool.ViewModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ClassDiagramTool.Commands
{
    class DeleteShapeCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> ShapeViewModels;
        private List<ShapeViewModel> SelectedShapeViewModels;

        public DeleteShapeCommand(ObservableCollection<ShapeViewModel> shapeViewModels, List<ShapeViewModel> selectedShapeViewModels)
        {
            ShapeViewModels = shapeViewModels;
            SelectedShapeViewModels = selectedShapeViewModels;
        }

        public void Execute()
        {
            foreach (ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Remove(ShapeViewModel);
            }

        }

        public void UnExecute()
        {
            foreach (ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Add(ShapeViewModel);
            }
        }
    }
}
