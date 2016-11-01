using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ClassDiagramTool.Model;
using ClassDiagramTool.UndoRedo;

namespace ClassDiagramTool.ViewModel.Shapes
{
    public abstract class ShapeViewModel : BaseViewModel, IShape
    {
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        protected Shape shape { get; }

        protected ShapeViewModel(Shape _shape)
        {
            shape = _shape;
        }

        public int Number => shape.Number;

        public double X
        {
            get { return shape.X; }
            set {
                shape.X = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }
        public double Y
        {
            get { return shape.Y; }
            set {
                shape.Y = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }

        public double Width
        {
            get { return shape.Width; }
            set {
                shape.Width = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }

        public double Height
        {
            get { return shape.Height; }
            set {
                shape.Height = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }

        public double CenterX
        {
            get { return X + Width / 2; }
            set {
                shape.X = value - Width / 2;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterX));
            }
        }

        public double CenterY
        {
            get { return Y + Height / 2; }
            set {
                shape.Y = value - (Height / 2);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CenterY));
            }
        }

        public EShape Type => shape.Type;

        public List<string> Data { get; set; }

        public override string ToString() => shape.ToString();
    }
}
