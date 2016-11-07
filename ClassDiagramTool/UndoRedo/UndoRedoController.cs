using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace ClassDiagramTool.UndoRedo
{
    public class UndoRedoController
    {
        private static readonly UndoRedoController self = new UndoRedoController();
        private readonly Stack<IUndoRedoCommand> undoStack = new Stack<IUndoRedoCommand>();
        private readonly Stack<IUndoRedoCommand> redoStack = new Stack<IUndoRedoCommand>();
       
        private UndoRedoController():base()
        {
        }

        public static UndoRedoController Instance => self;

        public RelayCommand UndoCommand => new RelayCommand(Undo, CanUndo);
        public RelayCommand RedoCommand => new RelayCommand(Redo, CanRedo);

        public void AddAndExecute(IUndoRedoCommand command)
        {
            undoStack.Push(command);
            redoStack.Clear();
            command.Execute();
            UpdateCommandStatus();
        }

        public bool CanUndo() => undoStack.Any();
        public void Undo()
        {
            IUndoRedoCommand command = undoStack.Pop();
            redoStack.Push(command);
            command.UnExecute();
            UpdateCommandStatus();
        }

        public bool CanRedo() => redoStack.Any();
        public void Redo()
        {
            IUndoRedoCommand command = redoStack.Pop();
            undoStack.Push(command);
            command.Execute();
            UpdateCommandStatus();
        }

        private void UpdateCommandStatus()
        {
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }
    }
}
