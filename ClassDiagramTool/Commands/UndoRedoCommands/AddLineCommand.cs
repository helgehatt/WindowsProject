using ClassDiagramTool.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace ClassDiagramTool.Commands
{
    class AddLineCommand : IUndoRedoCommand
    {
        private ObservableCollection<LineViewModel> LineViewModels;
        private LineViewModel LineViewModel;

        public AddLineCommand(ObservableCollection<LineViewModel> lineViewModels, LineViewModel lineViewModel)
        {
            LineViewModels = lineViewModels;
            LineViewModel = lineViewModel;
        }

        public void Execute()
        {
            LineViewModels.Add(LineViewModel);
        }

        public void UnExecute()
        {
            LineViewModels.Remove(LineViewModel);
        }
    }
}