using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class PasteCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> Shapes;

        public PasteCommand(ObservableCollection<ShapeViewModel> shapes, ShapeViewModel viewModel, MouseButtonEventArgs e)
        {
            UserControl data = Clipboard.GetDataObject() as UserControl;
            //data.DataContext as
            
            


        }

        public void Execute()
        {
            
            
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }

        
    }
}
