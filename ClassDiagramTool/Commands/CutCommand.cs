using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class CutCommand : IUndoRedoCommand
    {
        private UserControl SelectedElement;
        private DataObject DataObject;

        public CutCommand(ShapeViewModel viewModel, MouseButtonEventArgs e)
        {
            SelectedElement = e.Source as UserControl;
            DataObject data = new DataObject();
            //data.SetData
            //Call method to receive reference to the element in focus. 
        }

        public void Execute()
        {
            Clipboard.SetDataObject(SelectedElement);  
            //Remove from canvas 
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }

      
       
    }
}
