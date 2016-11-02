using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class AddShapeCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> shapes;
        private ShapeViewModel shape;

        public AddShapeCommand(ObservableCollection<ShapeViewModel> shapes, ShapeViewModel shape)
        {
            this.shapes = shapes;
            this.shape = shape;
        }

        public void Execute()
        {
            shapes.Add(shape);
        }

        public void UnExecute()
        {
            shapes.Remove(shape);
        }
    }
}
