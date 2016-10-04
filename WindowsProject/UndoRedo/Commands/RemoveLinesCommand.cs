using System.Collections.Generic;
using System.Collections.ObjectModel;
using WindowsProject.ViewModel;
using WindowsProject.ViewModel.Lines;

namespace WindowsProject.UndoRedo.Commands
{
    public class RemoveLinesCommand : IUndoRedoCommand
    {
        private ObservableCollection<LineViewModel> lines;
        
        private List<LineViewModel> linesToRemove;
        
        public RemoveLinesCommand(ObservableCollection<LineViewModel> _lines, List<LineViewModel> _linesToRemove) 
        {
            lines = _lines;
            linesToRemove = _linesToRemove;
        }
        
        public void Execute()
        {
            linesToRemove.ForEach(x => lines.Remove(x));
        }
        
        public void UnExecute()
        {
            linesToRemove.ForEach(x => lines.Add(x));
        }
    }
}
