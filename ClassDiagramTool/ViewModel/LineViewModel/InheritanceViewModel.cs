using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class InheritanceViewModel : LineViewModel
    {
        public InheritanceViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.Inheritance }, from, to)
        {
        }
    }
}
