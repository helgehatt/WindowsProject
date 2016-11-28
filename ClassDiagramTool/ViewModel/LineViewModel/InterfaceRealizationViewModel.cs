using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class InterfaceRealizationViewModel : LineViewModel
    {
        public InterfaceRealizationViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.Aggregation }, from, to)
        {
        }
    }
}
