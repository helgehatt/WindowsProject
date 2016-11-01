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

        protected LineViewModel(Line _line, ShapeViewModel _from, ShapeViewModel _to) 
            : this(_line)
        {
            to = _to;
            from = _from;
        }

        public int FromNumber => from.Number;
        public int ToNumber => to.Number;
        public string Label { get; set; }
        public ELine Type { get; set; }

        //public ShapeViewModel To
        //{
        //    get { return to; }
        //    set {
        //        to = value;
        //        line.ToNumber = value.Number;
        //        OnPropertyChanged();
        //        OnPropertyChanged(nameof(ToNumber));
        //    }
        //}
        //
        //public ShapeViewModel From
        //{
        //    get { return from; }
        //    set {
        //        from = value;
        //        line.FromNumber = value.Number;
        //        OnPropertyChanged();
        //        OnPropertyChanged(nameof(FromNumber));
        //    }
        //}
    }
}