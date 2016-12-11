using ClassDiagramTool.ViewModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace ClassDiagramTool.Commands
{
    class DeleteCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> ShapeViewModels;
        private ObservableCollection<LineViewModel>  LineViewModels;
        private List<ShapeViewModel> SelectedShapeViewModels;
        private List<LineViewModel> SelectedLineViewModels;

        public DeleteCommand(ObservableCollection<ShapeViewModel> shapeViewModels, ObservableCollection<LineViewModel> lineViewModels, List<ShapeViewModel> selectedShapeViewModels)
        {
            ShapeViewModels = shapeViewModels;
            LineViewModels  = lineViewModels;
            SelectedShapeViewModels = selectedShapeViewModels;
        }

        public void Execute()
        {
            SelectedLineViewModels = SelectedShapeViewModels.SelectMany(p => p.ConnectionPointViewModels.SelectMany(l => l.LineViewModels)).ToList();

            foreach (ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Remove(ShapeViewModel);
            }

            foreach (LineViewModel LineViewModel in SelectedLineViewModels)
            {
                LineViewModels.Remove(LineViewModel);
                LineViewModel.From.LineViewModels.Remove(LineViewModel);
                LineViewModel.To  .LineViewModels.Remove(LineViewModel);
            }

        }

        public void UnExecute()
        {
            foreach (ShapeViewModel ShapeViewModel in SelectedShapeViewModels)
            {
                ShapeViewModels.Add(ShapeViewModel);
            }

            foreach (LineViewModel LineViewModel in SelectedLineViewModels)
            {
                LineViewModels.Add(LineViewModel);
                LineViewModel.From.LineViewModels.Add(LineViewModel);
                LineViewModel.To  .LineViewModels.Add(LineViewModel);
            }
        }
    }
}
