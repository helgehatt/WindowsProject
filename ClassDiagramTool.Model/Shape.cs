using System.Collections.Generic;

namespace ClassDiagramTool.Model
{
    public class Shape : IShape
    {
        private static int number;

        public int Number { get; set; } = number++;
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; } = 250;
        public double Height { get; set; } = 100;
        public EShape Type { get; set; }

        public string Title { get; set; } = "Title";
        public List<string> Text { get; set; } = new List<string>() { "Text1", "Text2" };
    }
}
