namespace ClassDiagramTool.UndoRedo
{
    public interface IUndoRedoCommand
    {
        void Execute();
        void UnExecute();
    }
}
