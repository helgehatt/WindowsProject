using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public class DirectedAssociationViewModel : LineViewModel
    {
        public DirectedAssociationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.DirectedAssociation }, from, to)
        {
        }
    }
}
