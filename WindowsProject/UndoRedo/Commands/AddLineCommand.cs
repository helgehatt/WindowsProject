using System.Collections.ObjectModel;
using AdvancedWPFDemo.ViewModel;
using AdvancedWPFDemo.ViewModel.Lines;

namespace AdvancedWPFDemo.UndoRedo.Commands
{
    public class AddLineCommand : IUndoRedoCommand
    {
        private ObservableCollection<LineViewModel> lines;
        private LineViewModel line;
        
        public AddLineCommand(ObservableCollection<LineViewModel> _lines, LineViewModel _line) 
        { 
            lines = _lines;
            line = _line;
        }
        
        public void Execute()
        {
            lines.Add(line);
        }
        
        public void UnExecute()
        {
            lines.Remove(line);
        }
    }
}
