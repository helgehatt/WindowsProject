

namespace ClassDiagramTool.Commands
{
    public interface IUndoRedoCommand
    {
        void Execute();
        void UnExecute();
    }
}
