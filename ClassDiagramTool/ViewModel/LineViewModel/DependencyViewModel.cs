using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class DependencyViewModel : LineViewModel
    {
        public DependencyViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.Dependency }, from, to)
        {
        }
    }
}
