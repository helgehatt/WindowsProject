using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClassDiagramTool.Commands
{
    class ClipboardCommands
    {
        private MainViewModel MainViewModel;

        private SelectedObjectsController SelectedObjectsController => SelectedObjectsController.Instance;
        private UndoRedoController        UndoRedoController        => UndoRedoController.Instance;

        public ObservableCollection<ShapeViewModel> ShapeViewModels => MainViewModel.ShapeViewModels;

        public ClipboardCommands(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public void Cut()
        {
            List<UserControl> SelectedShapes = SelectedObjectsController.SelectionList;
                   
            List<Shape> Shapes = new List<Shape>();

            foreach (UserControl SelectedShape in SelectedShapes)
            {
                Shapes.Add((SelectedShape.DataContext as ShapeViewModel).Shape);                
            }

            Clipboard.Clear();
            Clipboard.SetData("Shapes", Shapes);
            List<ShapeViewModel> SelectedShapeViewModels = SelectedObjectsController.SelectionList.Select(o => o.DataContext as ShapeViewModel).ToList();
            UndoRedoController.Execute(new DeleteShapeCommand(ShapeViewModels, SelectedShapeViewModels));

        }

        public void Copy()
        {
            List<UserControl> SelectedShapes = SelectedObjectsController.SelectionList;

            List<Shape> Shapes = new List<Shape>();

            foreach (UserControl SelectedShape in SelectedShapes)
            {
                Shapes.Add((SelectedShape.DataContext as ShapeViewModel).Shape); 
            }

            Clipboard.Clear();
            Clipboard.SetData("Shapes", Shapes);
        }

        public void Paste()
        {           
            List<Shape> Shapes = Clipboard.GetData("Shapes") as List<Shape>;

            foreach (Shape Shape in Shapes)
            {
                ShapeViewModel ShapeViewModel = null;

                switch (Shape.Type)
                {
                    case EShape.Class:          ShapeViewModel = new ClassViewModel      (Shape); break;
                    case EShape.Enumeration:    ShapeViewModel = new EnumerationViewModel(Shape); break;
                    case EShape.Interface:      ShapeViewModel = new InterfaceViewModel  (Shape); break;
                }
                ShapeViewModels.Add(ShapeViewModel);
            }
        }
    }
}
