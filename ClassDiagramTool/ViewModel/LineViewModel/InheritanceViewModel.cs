using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.ViewModel
{
    public class InheritanceViewModel : LineViewModel
    {
        public InheritanceViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Inheritance }, from, to)
        {
        }
    }
}
