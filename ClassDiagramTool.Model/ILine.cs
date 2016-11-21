

namespace ClassDiagramTool.Model
{
    
    public interface ILine
    {
        int FromNumber { get; }
        int ToNumber { get; }
        string Label { get; }
        ELine Type { get; }
    }
}