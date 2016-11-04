using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Lines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class AddLineCommand : IUndoRedoCommand
    {
        private ObservableCollection<LineViewModel> lines;
        private LineViewModel line;

        public AddLineCommand(ObservableCollection<LineViewModel> lines, LineViewModel line)
        {
            this.lines = lines;
            this.line = line;
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
