using WindowsProject.Model;
using WindowsProject.ViewModel.Shapes;

namespace WindowsProject.ViewModel.Lines
{
    public class AssociationLineViewModel: LineViewModel
    {
        public AssociationLineViewModel() : this(new Line() {Type = ELine.Association }) { }

        public AssociationLineViewModel(Line line) : base(line)
        {
        }

        public AssociationLineViewModel(ShapeViewModel from, ShapeViewModel to) : base(new Line() {Type=ELine.Association}, from, to)
        {
        }
    }
}
