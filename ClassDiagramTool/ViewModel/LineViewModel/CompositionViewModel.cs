using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class CompositionViewModel : LineViewModel
    {
        public CompositionViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.Composition }, from, to)
        {
        }
    }
}
