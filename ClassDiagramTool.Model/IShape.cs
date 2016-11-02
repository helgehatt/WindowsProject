using System.Collections.Generic;

namespace ClassDiagramTool.Model
{
    public interface IShape
    {
        int Number { get; }
        double X { get; }
        double Y { get; }
        double Width { get; }
        double Height { get; set; }
        EShape Type { get; }

        string Title { get; }
        List<string> Text { get; }
    }
}