using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ClassDiagramTool.Commands;

namespace ClassDiagramTool.Tools
{
    public class UndoRedoController
    {
        private static readonly UndoRedoController self = new UndoRedoController();
        public static UndoRedoController Instance => self;

        private readonly Stack<IUndoRedoCommand> UndoStack = new Stack<IUndoRedoCommand>();
        private readonly Stack<IUndoRedoCommand> RedoStack = new Stack<IUndoRedoCommand>();
       
        private UndoRedoController() { }

        public RelayCommand UndoCommand => new RelayCommand(Undo, () => UndoStack.Any());
        public RelayCommand RedoCommand => new RelayCommand(Redo, () => RedoStack.Any());

        public void Execute(IUndoRedoCommand command)
        {
            UndoStack.Push(command);
            RedoStack.Clear();
            command.Execute();
            UpdateCommandStatus();
        }

        public void Undo()
        {
            IUndoRedoCommand Command = UndoStack.Pop();
            RedoStack.Push(Command);
            Command.UnExecute();
            UpdateCommandStatus();
        }
        
        public void Redo()
        {
            IUndoRedoCommand command = RedoStack.Pop();
            UndoStack.Push(command);
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
