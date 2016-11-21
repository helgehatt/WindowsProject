using System;
using System.Collections.Generic;

namespace ClassDiagramTool.Model
{
    [Serializable]
    public class Diagram
    {
        public List<Shape> Shapes { get; set; }
        public List<Line> Lines { get; set; }
    }
}
