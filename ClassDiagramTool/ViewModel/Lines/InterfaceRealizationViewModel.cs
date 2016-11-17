using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public class InterfaceRealizationViewModel : LineViewModel
    {
        public InterfaceRealizationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.InterfaceRealization }, from, to)
        {
        }
    }
}
