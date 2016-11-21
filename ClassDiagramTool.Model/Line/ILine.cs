

namespace ClassDiagramTool.Model
{
    
    public interface ILine
    {
        int FromNumber { get; }
        int ToNumber { get; }

        int FromPoint { get; }
        int ToPoint { get; }

        string Label { get; }

        ELine Type { get; }
    }
}