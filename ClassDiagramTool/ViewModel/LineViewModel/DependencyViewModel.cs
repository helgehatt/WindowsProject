using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;
using System.Diagnostics;

namespace ClassDiagramTool.ViewModel
{
    public class DependencyViewModel : LineViewModel
    {
        public DependencyViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Dependency }, from, to)
        {
        }
    }
}
