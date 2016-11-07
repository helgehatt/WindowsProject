using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        private readonly Line Line;
        protected ShapeViewModel To;
        protected ShapeViewModel From;
        
        protected LineViewModel(Line line)
        {
            Line = line;
        }

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to) 
            : this(line)
        {
            To = to;
            From = from;
        }

        public int FromNumber => From.Number;
        public int ToNumber => To.Number;
        public string Label { get; set; }
        public ELine Type { get; set; }
    }
}