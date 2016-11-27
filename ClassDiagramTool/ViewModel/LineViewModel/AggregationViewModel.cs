using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.ViewModel
{
    public class AggregationViewModel : LineViewModel
    {
        public AggregationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Aggregation }, from, to)
        {
        }
    }
}
