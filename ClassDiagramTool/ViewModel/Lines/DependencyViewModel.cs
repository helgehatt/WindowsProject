using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;
using System.Diagnostics;

namespace ClassDiagramTool.ViewModel.Lines
{
    public class DependencyViewModel : LineViewModel
    {
        public DependencyViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Dependency }, from, to)
        {
        }
    }
}
