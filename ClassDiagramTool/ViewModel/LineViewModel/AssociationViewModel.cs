using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class AssociationViewModel : LineViewModel
    {
        public AssociationViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.Association }, from, to)
        {
        }
    }
}
