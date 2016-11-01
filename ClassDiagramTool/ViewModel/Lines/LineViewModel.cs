using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        #region Fields
        private readonly Line _line;
        private ShapeViewModel _to;
        private ShapeViewModel _from;

        public int FromNumber => _from.Number;
        public int ToNumber => _to.Number;
        public string Label { get; set; }
        public ELine Type { get; set; }
        #endregion
        
        protected LineViewModel(Line line)
        {
            _line = line;
        }
        protected LineViewModel(Line line,ShapeViewModel from, ShapeViewModel to) : this(line)
        {
            To = to;
            From = from;
        }

        public ShapeViewModel To
        {
            get { return _to; }
            set {
                _to = value;
                _line.ToNumber = value.Number;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ToNumber));
            }
        }

        public ShapeViewModel From
        {
            get { return _from; }
            set {
                _from = value;
                _line.FromNumber = value.Number;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FromNumber));
            }
        }
    }
}