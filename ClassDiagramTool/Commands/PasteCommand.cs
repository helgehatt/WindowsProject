using ClassDiagramTool.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class PasteCommand : IUndoRedoCommand
    {
        public PasteCommand(MouseButtonEventArgs e)
        {

        }

        public void Execute()
        {
            IDataObject data = Clipboard.GetDataObject();

        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}
