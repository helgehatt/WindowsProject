using WindowsProject.Model;
using WindowsProject.ViewModel.Shapes;

namespace WindowsProject.ViewModel.Lines
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
