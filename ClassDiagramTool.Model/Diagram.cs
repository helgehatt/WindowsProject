using System.Collections.Generic;

namespace ClassDiagramTool.Model
{
    public class Diagram
    {
        public List<IShape> Shapes { get; set; }
        public List<ILine> Lines { get; set; }
    }
}
