using AdvancedWPFDemo.Model;
using AdvancedWPFDemo.ViewModel.Shapes;

namespace AdvancedWPFDemo.ViewModel.Lines
{
    public class SolidLineViewModel: LineViewModel
    {
        public SolidLineViewModel() : this(new Line() {Type = ELine.Solid }) { }
        public SolidLineViewModel(Line line) : base(line)
        {
        }

        public SolidLineViewModel( ShapeViewModel from, ShapeViewModel to) : base(new Line() {Type=ELine.Solid}, from, to)
        {
        }
    }
}
