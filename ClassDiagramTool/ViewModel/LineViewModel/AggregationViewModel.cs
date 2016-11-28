using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class AggregationViewModel : LineViewModel
    {
        public AggregationViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.Aggregation }, from, to)
        {
        }
    }
}
