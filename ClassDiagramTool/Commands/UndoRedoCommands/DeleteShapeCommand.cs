using ClassDiagramTool.ViewModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ClassDiagramTool.Commands
{
    class DeleteShapeCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> ShapeViewModels;
        private ObservableCollection<LineViewModel>  LineViewModels;
        private List<ShapeViewModel> SelectedShapeViewModels;

        public DeleteShapeCommand(ObservableCollection<ShapeViewModel> shapeViewModels, ObservableCollection<LineViewModel> lineViewModels, List<ShapeViewModel> selectedShapeViewModels)
        {
            ShapeViewModels = shapeViewModels;
            LineViewModels  = lineViewModels;
            SelectedShapeViewModels = selectedShapeViewModels;
        }

        public void Execute()
        {
            foreach (ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Remove(ShapeViewModel);
                
                foreach (var ConnectionPointViewModel in ShapeViewModel.ConnectionPointViewModels)
                    foreach (var LineViewModel in ConnectionPointViewModel.LineViewModels)
                        LineViewModels.Remove(LineViewModel);
            }

        }

        public void UnExecute()
        {
            foreach (ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Add(ShapeViewModel);

                foreach (var ConnectionPointViewModel in ShapeViewModel.ConnectionPointViewModels)
                    foreach (var LineViewModel in ConnectionPointViewModel.LineViewModels)
                        LineViewModels.Add(LineViewModel);
            }
        }
    }
}
