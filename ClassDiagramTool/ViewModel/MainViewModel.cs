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

        public ObservableCollection<BaseViewModel> Objects { get; }

        public SquareViewModel shape1, shape2;

        private Random rand = new Random();
        #endregion

        #region Commands
        public ICommand UndoCommand => UndoRedoController.UndoCommand;
        public ICommand RedoCommand => UndoRedoController.RedoCommand;

        public RelayCommand<MouseButtonEventArgs> AddObjectCommand => new RelayCommand<MouseButtonEventArgs>(OnAddObjectCommand, (e) => (e.Source is Canvas));
        //TODO Setup CanExecute to ConnectShape
        public RelayCommand<MouseButtonEventArgs> ConnectShapesCommand => new RelayCommand<MouseButtonEventArgs>((e) => new ConnectShapesCommand(), (e) => true);
        public RelayCommand<MouseButtonEventArgs> SelectShapeCommand => new RelayCommand<MouseButtonEventArgs>((e) => new SelectObjectCommand(e).Execute(), (e) => (e.Source is UserControl));

        #endregion

        #region CommandMethods
        private void OnAddObjectCommand(MouseButtonEventArgs e)
        {
            Canvas mainCanvas = e.Source as Canvas;

            Point position = Mouse.GetPosition(mainCanvas);

            BaseViewModel Object = new SquareViewModel() { CenterX = position.X, CenterY = position.Y, Title = "Title", Text = new List<string> { "text1", "text2", "text3" } };

            UndoRedoController.AddAndExecute(new AddObjectCommand(Objects, Object));
        }
        #endregion

        public MainViewModel() : base()
        {
            Objects = new ObservableCollection<BaseViewModel>();
            BaseViewModel Object = new SquareViewModel() { CenterX = 300, CenterY = 200, Title = "Title", Text = new List<string> { "text1", "text2", "text3" } };
            Objects.Add(Object);
        }
    }


}
