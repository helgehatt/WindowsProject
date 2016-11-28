


namespace ClassDiagramTool.Model
{
    
    public interface ILine
    {
        int FromShape { get; }
        int ToShape { get; }

        int FromPoint { get; }
        int ToPoint { get; }

        ELine Type { get; }

        string Label { get; }
    }
}