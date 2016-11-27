using ClassDiagramTool.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClassDiagramTool.Commands
{
    class AddShapeCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> ShapeViewModels;
        private List<ShapeViewModel> SelectedShapeViewModels;

        public AddShapeCommand(ObservableCollection<ShapeViewModel> shapeViewModels, List<ShapeViewModel> selectedShapeViewModels)
        {
            ShapeViewModels = shapeViewModels;
            SelectedShapeViewModels = selectedShapeViewModels;
        }

        public void Execute()
        {
            foreach(ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Add(ShapeViewModel);
            }
            
        }

        public void UnExecute()
        {
            foreach (ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Remove(ShapeViewModel);
            }
        }
    }
}