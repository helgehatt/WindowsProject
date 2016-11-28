using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class DirectedAssociationViewModel : LineViewModel
    {
        public DirectedAssociationViewModel(ConnectionPointViewModel from, ConnectionPointViewModel to) 
            : base(new Line() { Type = ELine.DirectedAssociation }, from, to)
        {
        }
    }
}
