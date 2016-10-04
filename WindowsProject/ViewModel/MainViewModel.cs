using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AdvancedWPFDemo.Model;
using AdvancedWPFDemo.UndoRedo;
using AdvancedWPFDemo.UndoRedo.Commands;
using AdvancedWPFDemo.View.ExtendedComponents;
using AdvancedWPFDemo.ViewModel.Lines;
using AdvancedWPFDemo.ViewModel.Shapes;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmCommandBindings;
using _02350AdvancedDemo.ViewModel;

namespace AdvancedWPFDemo.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //    private bool _isLinesEnabled = true;
        //    private bool _isShapesEnabled = true;
        private EGlobalShapeStatus _globalShapeStatus;
        private bool _isAddingLine;
        private bool _isAddingLineBtnPressed;
        private ShapeViewModel _firstShapeToConnect;
        private bool _isAddingCirclePressed;
        private bool _isAddingSquarePressed;
        //private Dictionary<int, Point> initialShapePositions = new Dictionary<int, Point>();
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        #region BindableProperties
        public ObservableCollection<LineViewModel> Lines { get; }
        public ObservableShapesCollection Shapes { get; }
        public bool IsAddingLineBtnPressed
        {
            get { return _isAddingLineBtnPressed; }
            set
            {
                _isAddingLineBtnPressed = value;
                if (!value)
                    _firstShapeToConnect = null;
                if (value && (IsAddingSquarePressed) || IsAddingCirclePressed)
                {
                    IsAddingSquarePressed = false;
                    IsAddingCirclePressed = false;
                }
                    

                OnPropertyChanged();
                AddLineBetweenShapesCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsAddingCirclePressed
        {
            get { return _isAddingCirclePressed; }
            set {
                _isAddingCirclePressed = value;
                if (value && IsAddingSquarePressed)
                    IsAddingSquarePressed = false;
                OnPropertyChanged();
                CreateShapeInCanvasCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsAddingSquarePressed
        {
            get { return _isAddingSquarePressed; }
            set
            {
                _isAddingSquarePressed = value;
                if (value && IsAddingCirclePressed)
                    IsAddingCirclePressed = false;
                OnPropertyChanged();
                CreateShapeInCanvasCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsAddingLine
        {
            get { return _isAddingLine; }
            set
            {
                _isAddingLine = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands


        public ICommand UndoCommand => UndoRedoController.UndoCommand;
        public ICommand RedoCommand => UndoRedoController.RedoCommand;
        public ICommand RemoveShapesCommand => new RelayCommand<object>(OnRemoveShapes, s => Shapes.Any());
        public ICommand OpenCommand => new RelayCommand(OnOpenDocument);

        

        public ICommand DeselectAllCommand => new RelayCommand<MouseButtonEventArgs>(DeselectAll);
        public RelayCommand<ShapeViewModel> AddLineBetweenShapesCommand => new RelayCommand<ShapeViewModel>(OnAddLineBetweenShapes, s => IsAddingLineBtnPressed);

        public RelayCommand<MouseButtonEventArgs> CreateShapeInCanvasCommand
            => new RelayCommand<MouseButtonEventArgs>(OnClickCreateShape, CanCreateShape);

        #endregion

        #region CommandMethods
        private void OnRemoveShapes(object obj)
        {
            var a = 3;
        }
        private void OnOpenDocument()
        {
            throw new NotImplementedException();
        }
        private void OnAddLineBetweenShapes(ShapeViewModel shapeViewModel)
        {
            if (_firstShapeToConnect == null)
                _firstShapeToConnect = shapeViewModel;
            else
            {
                if (shapeViewModel != _firstShapeToConnect)
                {
                    UndoRedoController.AddAndExecute(
                        new AddLineCommand(Lines,
                            new SolidLineViewModel(_firstShapeToConnect, shapeViewModel))
                    );
                    IsAddingLineBtnPressed = false;
                }
            }
        }

        private bool CanCreateShape(MouseButtonEventArgs e)
        {
            return IsAddingCirclePressed || IsAddingSquarePressed;
        }

        

        private void OnClickCreateShape(MouseButtonEventArgs e)
        {
            ShapeViewModel shape = null; ;
            var pos = e.MouseDevice.GetPosition(e.Source as IInputElement);
            if (IsAddingCirclePressed)
            {
                shape = new CircleViewModel() { Width = 80, Height = 80, X = pos.X, Y = pos.Y };
                IsAddingCirclePressed = false;
            } else if (IsAddingSquarePressed)
            {
                shape = new SquareViewModel() { Width = 200, Height = 150, X = pos.X, Y = pos.Y };
                IsAddingSquarePressed = false;
            }
            if (shape != null)
                UndoRedoController.AddAndExecute(new AddShapeCommand(Shapes, shape));
        }
        #endregion

        private void DeselectAll(MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is Canvas)) return;
            e.Handled = true;

            Shapes.GlobalStatus = EGlobalShapeStatus.DeSelectAll;

        }


        public MainViewModel() : base()
        {
            Shapes = new ObservableShapesCollection() {
                new CircleViewModel(){  X = 30, Y = 40, Width = 80, Height = 80, Data = new List<string> { "text1", "text2", "text3" } },
                new SquareViewModel() { X = 140, Y = 230, Width = 200, Height = 100, Data = new List<string> { "text1", "text2", "text3" } }
            };

            Lines = new ObservableCollection<LineViewModel>() {
                new SolidLineViewModel(Shapes[0],Shapes[1]) {Label="lineText"}
            };
        }
        //the following section is not fully used at this version. do ignore
        #region ActionMonitor


        private Stack<EAction> ActiveActions => new Stack<EAction>();

        private void StartAction(EAction action)
        {
            switch (action)
            {
                case EAction.AddCircle:
                    break;
                case EAction.AddSquare:
                    break;
                case EAction.AddSolidLine:
                    break;
                case EAction.AddDashedLine:
                    break;
                case EAction.Selection:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
            ActiveActions.Push(action);
        }

        private void CompleteAction(EAction action)
        {
            var topAction = ActiveActions.Peek();
            var isPrimary = action < topAction;
            if (!ActiveActions.Any() && action!=topAction ) return;
            switch (ActiveActions.Pop())
            {
                case EAction.AddCircle:
                    break;
                case EAction.AddSquare:
                    break;
                case EAction.AddSolidLine:
                    break;
                case EAction.AddDashedLine:
                    break;
                case EAction.Selection:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

    }
}