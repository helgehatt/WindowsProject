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

        // Test fields
        private Random rand = new Random();
        private List<int> numbers = new List<int>();
        #endregion

        #region Commands
        public ICommand UndoCommand => UndoRedoController.UndoCommand;
        public ICommand RedoCommand => UndoRedoController.RedoCommand;

        public RelayCommand PrintHelloWorldCommand => new RelayCommand(OnHelloWorldCommand, () => true);
        #endregion

        #region CommandMethods
        private void OnHelloWorldCommand()
        {
            var Shape = new SquareViewModel() { X = rand.Next(1, 500), Y = rand.Next(1, 500), Width = 80, Height = 80, Data = new List<string> { "text1", "text2", "text3" } };
            UndoRedoController.AddAndExecute(new AddShapeCommand(Shapes, Shape));
        }
        #endregion

        public MainViewModel() : base()
        {
            Shapes = new ObservableCollection<ShapeViewModel>() {
                new SquareViewModel(){ X =  30, Y =  40, Width =  80, Height =  80, Data = new List<string> { "text1", "text2", "text3" } },
                new SquareViewModel(){ X = 140, Y = 230, Width = 200, Height = 100, Data = new List<string> { "text1", "text2", "text3" } }
            };

            Lines = new ObservableCollection<LineViewModel>() {
                new SolidLineViewModel(Shapes[0],Shapes[1]) {Label="lineText"}
            };
        }
    }


}
