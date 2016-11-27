using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.ViewModel
{
    public class InterfaceRealizationViewModel : LineViewModel
    {
        public InterfaceRealizationViewModel(ShapeViewModel fromShape, int fromPoint, ShapeViewModel toShape, int toPoint) 
            : base(new Line() { Type = ELine.Aggregation }, fromShape, fromPoint, toShape, toPoint)
        {
        }
    }
}
