using ClassDiagramTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Commands
{
    class AddCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> ShapeViewModels;
        private ObservableCollection<LineViewModel> LineViewModels;
        private List<ShapeViewModel> AddedShapeViewModels;
        private List<LineViewModel> AddedLineViewModels;

        public AddCommand(ObservableCollection<ShapeViewModel> shapeViewModels, ObservableCollection<LineViewModel> lineViewModels, 
            List<ShapeViewModel> addedShapeViewModels, List<LineViewModel> addedLineViewModels)
        {
            ShapeViewModels = shapeViewModels;
            LineViewModels = lineViewModels;
            AddedShapeViewModels = addedShapeViewModels;
            AddedLineViewModels = addedLineViewModels;
        }

        public void Execute()
        {
            foreach (var ShapeViewModel in AddedShapeViewModels)
                ShapeViewModels.Add(ShapeViewModel);

            foreach (var LineViewModel in AddedLineViewModels)
                LineViewModels.Add(LineViewModel);
        }

        public void UnExecute()
        {
            foreach (var ShapeViewModel in AddedShapeViewModels)
                ShapeViewModels.Remove(ShapeViewModel);

            foreach (var LineViewModel in AddedLineViewModels)
                LineViewModels.Remove(LineViewModel);
        }
    }
}
