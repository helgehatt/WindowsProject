
namespace ClassDiagramTool.Model
{
    public class Line : ILine
    {
        public int FromNumber { get; set; }
        public int ToNumber { get; set; }

        public int FromPoint { get; set; }
        public int ToPoint { get; set; }

        public ELine Type { get; set; }

        public string Label { get; set; }
    }
}
