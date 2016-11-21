using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel;
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
    class CopyCommand : IUndoRedoCommand
    {
        private UserControl SelectedElement;
        private SelectedObjectsCollection SelectedObjectsCollection => SelectedObjectsCollection.Instance;


        public CopyCommand(ShapeViewModel viewModel, MouseButtonEventArgs e)
        {
            if(e == null)
            {
                //Get int?
               // SelectedElement = SelectedObjectsCollection.Get() as UserControl;
                

            } else
            {
                SelectedElement = e.Source as UserControl;
            }

        }

        public void Execute()
        {
            Clipboard.SetDataObject(SelectedElement);
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}
