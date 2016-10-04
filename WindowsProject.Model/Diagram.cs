using System.Collections.Generic;

namespace WindowsProject.Model
{
    public class Diagram
    {
        public List<IShape> Shapes { get; set; }
        public List<Line> Lines { get; set; }
    }
}
