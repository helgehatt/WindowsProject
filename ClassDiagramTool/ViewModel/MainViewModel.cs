using ClassDiagramTool.Commands;
using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Lines;
using ClassDiagramTool.ViewModel.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ClassDiagramTool.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        #region Fields
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        public ObservableCollection<LineViewModel> Lines { get; }
        public ObservableCollection<ShapeViewModel> Shapes { get; }

        public ClickAddShape ClickAddShape = new ClickAddShape();

        private Random rand = new Random();
        #endregion

        #region Commands
        public ICommand UndoCommand => UndoRedoController.UndoCommand;
        public ICommand RedoCommand => UndoRedoController.RedoCommand;

        //public RelayCommand PrintHelloWorldCommand => new RelayCommand(OnAddShapeCommand, () => true);
        public RelayCommand<MouseButtonEventArgs> AddShapeCommand => new RelayCommand<MouseButtonEventArgs>(OnAddShapeCommand, b => true);
        #endregion

        #region CommandMethods
        private void OnAddShapeCommand(object parameter)
        {
            SquareViewModel Shape;
            if (parameter != null)
            {
                MouseButtonEventArgs e = parameter as MouseButtonEventArgs;
                Canvas mainCanvas = e.Source as Canvas;

                Point position = Mouse.GetPosition(mainCanvas);

                Shape = new SquareViewModel() { Width = 200, Height = 100, CenterX = position.X, CenterY = position.Y, Data = new List<string> { "text1", "text2", "text3" } };
            } else {
                Shape = new SquareViewModel() { X = rand.Next(1, 200), Y = rand.Next(1, 100), Width = 200, Height = 100, Data = new List<string> { "text1", "text2", "text3" } };
            }
            UndoRedoController.AddAndExecute(new AddShapeCommand(Shapes, Shape));
        }
        #endregion

        public MainViewModel() : base()
        {
            Shapes = new ObservableCollection<ShapeViewModel>();
            Lines = new ObservableCollection<LineViewModel>();
        }
    }


}
