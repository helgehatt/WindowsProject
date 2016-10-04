using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AdvancedWPFDemo.Model;
using AdvancedWPFDemo.UndoRedo;
using AdvancedWPFDemo.UndoRedo.Commands;
using GalaSoft.MvvmLight.Command;

namespace AdvancedWPFDemo.ViewModel.Shapes
{
    public abstract class ShapeViewModel : BaseViewModel, IShape,IEquatable<ShapeViewModel>,IEqualityComparer<ShapeViewModel>,IGlobalShapeStatusChanged
    {
        #region Fields
        private bool _isSelected;
        private Point _initialMousePostion;
        private bool _isMoving;
        private bool _isConnectingShapes;
        private Point _initialShapePostion;

        #endregion


        #region Bindable Properties

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnectingShapes
        {
            get { return _isConnectingShapes; }
            set
            {
                _isConnectingShapes = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Command Declarations

        public ICommand OnMouseLeftBtnDownCommand => new RelayCommand<MouseButtonEventArgs>(OnMouseLeftBtnDown);
        public ICommand OnMouseMoveCommand => new RelayCommand<UIElement>(OnMouseMove);
        public ICommand OnMouseLeftBtnUpCommand => new RelayCommand<MouseButtonEventArgs>(OnMouseLeftUp);
        #endregion
        private void OnMouseLeftBtnDown(MouseButtonEventArgs e)
        {
            var visual = e.Source as UIElement;
            if (visual == null) return;
            if (!IsSelected)
            {
                IsSelected = true;
                visual.Focus();
                e.Handled = true;
                return;
            }
            if (!IsSelected && e.MouseDevice.Target.IsMouseCaptured) return;
            e.MouseDevice.Target.CaptureMouse();
            _initialMousePostion = Mouse.GetPosition(visual);
            _initialShapePostion = new Point(X, Y);
            //_canvas = VisualTreeHelper.GetParent(visual) as UIElement;
            _isMoving = true;
        }
        private void OnMouseMove(UIElement visual)
        {

            if (!_isMoving) return;

            var pos = Mouse.GetPosition(VisualTreeHelper.GetParent(visual)as IInputElement);
            X = pos.X - _initialMousePostion.X;
            Y = pos.Y - _initialMousePostion.Y;
            
        }


        private void OnMouseLeftUp(MouseButtonEventArgs e)
        {
            if (!_isMoving) return;
            UndoRedoController.AddAndExecute(new MoveShapeCommand(this, _initialShapePostion, new Point(X, Y)));
            _isMoving = false;
            Mouse.Capture(null);
            e.Handled = true;

        }
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        

        #region Shape Wrapper




        protected Shape Shape { get; }
        protected ShapeViewModel(Shape shape)
        {
            Shape = shape;
        }
        public int Number => Shape.Number;
        public EShape Type => Shape.Type;
        public List<string> Data { get; set; }
        public double Height
        {
            get { return Shape.Height; }
            set
            {
                Shape.Height = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }
        

        public double Width
        {
            get { return Shape.Width; }
            set
            {
                Shape.Width = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }

        public double X
        {
            get { return Shape.X; }
            set
            {
                Shape.X = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }
        public double Y
        {
            get { return Shape.Y; }
            set
            {
                Shape.Y = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }
        public double CenterX => Width / 2 + X;

        public double CenterY => Height / 2 + Y;


        #region Equality definitions
        public override bool Equals(object obj) => Equals(obj as ShapeViewModel);
        public bool Equals(ShapeViewModel other) => Number == other?.Number;
        public bool Equals(ShapeViewModel x, ShapeViewModel y) => x?.Number == y?.Number;

        public override int GetHashCode()
        {
            return Number;
        }
        public int GetHashCode(ShapeViewModel obj)
        {
            if (obj == null) throw new ArgumentNullException($"{nameof(GetHashCode)}() can not be null");
            return obj.GetHashCode();
        }


        #endregion

        public override string ToString() => Shape.ToString();
        //used for setting global markers on all shapes. 
        //this is an aproach where all shapes GlobalStatus method will Be Calld. It Should be able to be done faster
        public void GlobalStatus(ObservableShapesCollection sender, GlobalStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case EGlobalShapeStatus.Default:
                    IsConnectingShapes = false;
                    IsSelected = false;
                    break;
                case EGlobalShapeStatus.EnableAll:
                    throw new NotImplementedException();
                    break;
                case EGlobalShapeStatus.DisableAll:
                    throw new NotImplementedException();
                    break;
                case EGlobalShapeStatus.SelectAll:
                    IsSelected = true;
                    break;
                case EGlobalShapeStatus.DeSelectAll:
                    IsSelected = false;
                    break;
                case EGlobalShapeStatus.ConnectShapes:
                    IsConnectingShapes = !IsConnectingShapes;
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

       
    }
}
