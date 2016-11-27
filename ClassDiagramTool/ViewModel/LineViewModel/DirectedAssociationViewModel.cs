using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.ViewModel
{
    public class DirectedAssociationViewModel : LineViewModel
    {
        public DirectedAssociationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.DirectedAssociation }, from, to)
        {
        }
    }
}
