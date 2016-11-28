using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Commands
{
    class AddConnectionPointCommand : IUndoRedoCommand
    {
        private ObservableCollection<ConnectionPointViewModel> ConnectionPointViewModels;
        private ConnectionPointViewModel ConnectionPointViewModel;

        public AddConnectionPointCommand(ObservableCollection<ConnectionPointViewModel> connectionPointViewModels, ConnectionPointViewModel connectionPointViewModel)
        {
            ConnectionPointViewModels = connectionPointViewModels;
            ConnectionPointViewModel  = connectionPointViewModel;
        }

        public void Execute()
        {
            ConnectionPointViewModels.Add(ConnectionPointViewModel);
        }

        public void UnExecute()
        {
            ConnectionPointViewModels.Remove(ConnectionPointViewModel);
        }
    }
}
