using System.Collections.Generic;

namespace ClassDiagramTool.Model
{
    public class Shape : IShape
    {
        private static int _number;

        public int Number { get; set; } = _number++;
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public EShape Type { get; set; }

        public string Title { get; set; }
        public List<string> Data { get; set; }
    }
}
