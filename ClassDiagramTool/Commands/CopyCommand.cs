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
    class CopyCommand : IUndoRedoCommand
    {
        private UserControl SelectedElement;

        public CopyCommand(MouseButtonEventArgs e)
        {
            SelectedElement = e.Source as UserControl;

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
