using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class InterfaceRealizationViewModel : LineViewModel
    {
        public InterfaceRealizationViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.InterfaceRealization }, from, to)
        {
        }
    }
}
