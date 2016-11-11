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
        private ShapeViewModel From;
        public static bool IsAddingLine = false; // Replaced by selection

        public ObservableCollection<BaseViewModel> Objects { get; }
        #endregion

        #region Commands
        public ICommand UndoCommand => UndoRedoController.UndoCommand;
        public ICommand RedoCommand => UndoRedoController.RedoCommand;
        
        public RelayCommand<MouseButtonEventArgs> MouseLeftButtonDown => new RelayCommand<MouseButtonEventArgs>(OnMouseLeftButtonDown);
        public RelayCommand IsAddingLineChange => new RelayCommand(() => { IsAddingLine = !IsAddingLine; });
        #endregion

        #region CommandMethods
        private void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (IsAddingLine) OnAddLineCommand(e);
            else              OnAddShapeCommand(e);
        }

        private void OnAddShapeCommand(MouseButtonEventArgs e)
        {
            Canvas MainCanvas = e.Source as Canvas;

            Point Position = Mouse.GetPosition(MainCanvas);

            BaseViewModel Object = new SquareViewModel() { CenterX = Position.X, CenterY = Position.Y, Title = "Title", Text = new List<string> { "text1", "text2" } };

            UndoRedoController.AddAndExecute(new AddObjectCommand(Objects, Object));
        }

        private void OnAddLineCommand(MouseButtonEventArgs e)
        {
            UserControl UserControl = e.Source as UserControl;
            if (UserControl == null) return;

            ShapeViewModel Object = UserControl.DataContext as ShapeViewModel;

                 if (Object == null) Debug.WriteLine("OnConnectShapesCommand, DataContext=" + (e.Source as UserControl).DataContext);
            else if (From   == null) From = Object;
            else if (From   != Object)
            {
                UndoRedoController.AddAndExecute(new AddObjectCommand(Objects, new SolidLineViewModel(From, Object)));
                From = null;
            }            
        }
        #endregion

        public MainViewModel() : base()
        {
            Objects = new ObservableCollection<BaseViewModel>();
        }
    }


}
