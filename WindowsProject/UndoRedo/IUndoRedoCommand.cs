namespace WindowsProject.UndoRedo
{
    public interface IUndoRedoCommand
    {
        void Execute();
        void UnExecute();
    }
}
