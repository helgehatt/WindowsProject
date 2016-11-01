using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ClassDiagramTool.Model;
using ClassDiagramTool.UndoRedo;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;

namespace ClassDiagramTool.ViewModel.Shapes
{
    public abstract class ShapeViewModel : BaseViewModel, IShape
    {
        #region Fields
        private bool _isSelected;
        private Point _initialMousePostion;
        private bool _isMoving;
        private bool _isConnectingShapes;
        private Point _initialShapePostion;

        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        public override string ToString() => Shape.ToString();
        #endregion

        #region Bindable Properties
        public bool IsSelected
        {
            get { return _isSelected; }
            set {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnectingShapes
        {
            get { return _isConnectingShapes; }
            set {
                _isConnectingShapes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand OnMouseLeftBtnDownCommand => new RelayCommand<MouseButtonEventArgs>(OnMouseLeftBtnDown);
        public ICommand OnMouseMoveCommand => new RelayCommand<UIElement>(OnMouseMove);
        public ICommand OnMouseLeftBtnUpCommand => new RelayCommand<MouseButtonEventArgs>(OnMouseLeftUp);
        #endregion

        #region CommandMethods
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
            //UndoRedoController.AddAndExecute(new MoveShapeCommand(this, _initialShapePostion, new Point(X, Y)));
            _isMoving = false;
            Mouse.Capture(null);
            e.Handled = true;
        }
        #endregion
        
        #region Shape Wrapper
        protected Shape Shape { get; }
        protected ShapeViewModel(Shape shape)
        {
            Shape = shape;
        }
        public int Number => Shape.Number;
        public EShape Type => Shape.Type;
        public List<string> Data { get; set; }

        public double X
        {
            get { return Shape.X; }
            set {
                Shape.X = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }
        public double Y
        {
            get { return Shape.Y; }
            set {
                Shape.Y = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }

        public double Width
        {
            get { return Shape.Width; }
            set {
                Shape.Width = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }

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

        public double CenterX => Width / 2 + X;
        public double CenterY => Height / 2 + Y;
        #endregion
    }
}
