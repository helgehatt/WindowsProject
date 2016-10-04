using System.Collections.Generic;

namespace AdvancedWPFDemo.Model
{
    public class Diagram
    {
        public List<IShape> Shapes { get; set; }
        public List<Line> Lines { get; set; }
    }
}
