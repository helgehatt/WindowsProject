using ClassDiagramTool.UndoRedo;
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
    class PasteCommand : IUndoRedoCommand
    {
        public PasteCommand(MouseButtonEventArgs e)
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
