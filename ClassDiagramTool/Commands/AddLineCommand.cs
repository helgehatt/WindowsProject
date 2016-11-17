using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Lines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class AddLineCommand : IUndoRedoCommand
    {
        private ObservableCollection<LineViewModel> Lines;
        private LineViewModel Line;

        public AddLineCommand(ObservableCollection<LineViewModel> lines, LineViewModel line)
        {
            Lines = lines;
            Line = line;
        }

        public void Execute()
        {
            Lines.Add(Line);
        }

        public void UnExecute()
        {
            Lines.Remove(Line);
        }
    }
}