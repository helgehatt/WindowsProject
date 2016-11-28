using System;

namespace ClassDiagramTool.Model
{
    public class Line : ILine
    {
        public int FromShape { get; set; }
        public int ToShape { get; set; }

        public int FromPoint { get; set; }
        public int ToPoint { get; set; }

        public ELine Type { get; set; }

        public string Label { get; set; }
    }
}
