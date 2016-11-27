using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.ViewModel
{
    public class CompositionViewModel : LineViewModel
    {
        public CompositionViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Composition }, from, to)
        {
        }
    }
}
