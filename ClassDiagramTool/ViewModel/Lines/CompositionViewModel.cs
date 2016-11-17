using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public class CompositionViewModel : LineViewModel
    {
        public CompositionViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Composition }, from, to)
        {
        }
    }
}
