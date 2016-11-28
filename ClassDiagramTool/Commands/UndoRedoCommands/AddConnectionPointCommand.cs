using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Commands
{
    class AddConnectionPointCommand : IUndoRedoCommand
    {
        private ShapeViewModel ShapeViewModel;
        private ConnectionPoint ConnectionPoint;
        private ConnectionPointViewModel ConnectionPointViewModel;

        public AddConnectionPointCommand(ShapeViewModel shapeViewModel, ConnectionPoint connectionPoint)
        {
            ShapeViewModel = shapeViewModel;
            ConnectionPoint = connectionPoint;
            ConnectionPointViewModel = new ConnectionPointViewModel(connectionPoint, shapeViewModel);
        }

        public void Execute()
        {
            ShapeViewModel.Points.Add(ConnectionPoint);
            ShapeViewModel.ConnectionPointViewModels.Add(ConnectionPointViewModel);
        }

        public void UnExecute()
        {
            ShapeViewModel.Points.Remove(ConnectionPoint);
            ShapeViewModel.ConnectionPointViewModels.Remove(ConnectionPointViewModel);
        }
    }
}
