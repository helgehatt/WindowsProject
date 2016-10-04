

namespace WindowsProject.Model
{
    public interface ILine
    {
        int FromNumber { get; }
        string Label { get; }
        int ToNumber { get; }
        ELine Type { get; }
    }
}