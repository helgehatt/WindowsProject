using System;
using System.Collections.Generic;

namespace ClassDiagramTool.Model
{
 
    public interface IShape
    {
        int Number { get; }

        double X { get; }
        double Y { get; }

        double Width { get; }
        double Height { get; }

        EShape Type { get; }

        string Title { get; }
        List<string> Text { get; }

        List<ConnectionPoint> Points { get; }
    }
}