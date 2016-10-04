using WindowsProject.Model;
using WindowsProject.ViewModel.Shapes;

namespace WindowsProject.ViewModel.Lines
{
    public abstract class LineViewModel :BaseViewModel, ILine
    {
        private ShapeViewModel _to;
        private ShapeViewModel _from;
        private readonly Line _line;
        public int FromNumber => _from.Number;
        public string Label { get; set; }
        public int ToNumber => _to.Number;
        public ELine Type { get; set; }

        protected LineViewModel(Line line)
        {
            _line = line;
        }
        protected LineViewModel(Line line,ShapeViewModel from, ShapeViewModel to):this(line)
        {
            To = to;
            From = from;
        }
        public ShapeViewModel To
        {
            get { return _to; }
            set
            {
                _to = value;
                _line.ToNumber = value.Number;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ToNumber));
            }
        }

        public ShapeViewModel From
        {
            get{return _from;}
            set
            {
                _from = value;
                _line.FromNumber = value.Number;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FromNumber));
            }
        }
    }
}