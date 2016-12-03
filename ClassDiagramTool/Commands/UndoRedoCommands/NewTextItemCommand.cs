using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassDiagramTool.ViewModel;

namespace ClassDiagramTool.Commands
{
    class NewTextItemCommand : IUndoRedoCommand
    {
        private ShapeViewModel ShapeViewModel;
        private Model.TextItem TextItem;

        public NewTextItemCommand(ShapeViewModel shapeViewModel)
        {
            ShapeViewModel = shapeViewModel;
            TextItem = new Model.TextItem() { Value = "New Item" };
        }

        public void Execute()
        {
            ShapeViewModel.Text.Add(TextItem);
        }

        public void UnExecute()
        {
            ShapeViewModel.Text.Remove(TextItem);
        }
    }
}
