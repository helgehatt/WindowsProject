using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.ViewModel
{
    public class InterfaceRealizationViewModel : LineViewModel
    {
        public InterfaceRealizationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.InterfaceRealization }, from, to)
        {
        }
    }
}
