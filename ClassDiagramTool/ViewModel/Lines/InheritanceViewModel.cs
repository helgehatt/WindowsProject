using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public class InheritanceViewModel : LineViewModel
    {
        public InheritanceViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Inheritance }, from, to)
        {
        }
    }
}
