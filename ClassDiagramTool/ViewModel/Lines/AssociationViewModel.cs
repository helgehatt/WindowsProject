using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public class AssociationViewModel : LineViewModel
    {
        public AssociationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Association }, from, to)
        {
        }
    }
}
