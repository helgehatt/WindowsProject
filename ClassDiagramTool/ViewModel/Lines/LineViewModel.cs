using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        private readonly Line line;
        private ShapeViewModel to;
        private ShapeViewModel from;
        
        protected LineViewModel(Line line)
        {
            this.line = line;
        }

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to) 
            : this(line)
        {
            this.to = to;
            this.from = from;
        }

        public int FromNumber => from.Number;
        public int ToNumber => to.Number;
        public string Label { get; set; }
        public ELine Type { get; set; }
    }
}