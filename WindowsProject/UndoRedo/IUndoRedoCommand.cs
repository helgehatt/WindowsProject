namespace AdvancedWPFDemo.UndoRedo
{
    public interface IUndoRedoCommand
    {
        void Execute();
        void UnExecute();
    }
}
