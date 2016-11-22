using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public class AggregationViewModel : LineViewModel
    {
        public AggregationViewModel(ShapeViewModel from, ShapeViewModel to) 
            : base(new Line() { Type = ELine.Aggregation }, from, to)
        {
        }

        public AggregationViewModel(ConnectionPoint from, ConnectionPoint to)
            : base(new Line() { Type = ELine.Aggregation }, from, to)
        {
        }
    }
}
