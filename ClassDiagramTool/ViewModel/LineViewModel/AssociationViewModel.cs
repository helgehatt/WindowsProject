using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.ViewModel
{
    public class AssociationViewModel : LineViewModel
    {
        public AssociationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Association }, from, to)
        {
        }
    }
}
