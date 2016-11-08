using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ClassDiagramTool.Model;
using ClassDiagramTool.UndoRedo;
using GalaSoft.MvvmLight.CommandWpf;
using ClassDiagramTool.Commands;
using System.Diagnostics;
using System.Windows.Controls;

namespace ClassDiagramTool.ViewModel.Shapes
{
    public abstract class ShapeViewModel : BaseViewModel, IShape
    {
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        public RelayCommand<MouseButtonEventArgs> MoveShapeCommand => new RelayCommand<MouseButtonEventArgs>((e) => { UndoRedoController.AddAndExecute(new MoveShape(this, e)); }, (e) => false);
        public RelayCommand<MouseButtonEventArgs> EditTextCommand => new RelayCommand<MouseButtonEventArgs>((e) => UndoRedoController.AddAndExecute(new EditText(e)), (e) => false);

        protected Shape Shape { get; }

        protected ShapeViewModel(Shape shape) {
            Shape = shape;
            Width = 250;
            Height = 100;
        }

        public int Number => Shape.Number;

        public double X {
            get { return Shape.X; }
            set { Shape.X = value;
                OnPropertyChanged(); }
        }

        public double Y {
            get { return Shape.Y; }
            set { Shape.Y = value;
                OnPropertyChanged(); }
        }

        public double Width {
            get { return Shape.Width; }
            set { Shape.Width = value; }
        }

        public double Height {
            get { return Shape.Height; }
            set { Shape.Height = value; }
        }

        public double CenterX {
            get { return X + Width / 2; }
            set { Shape.X = value - Width / 2; }
        }

        public double CenterY {
            get { return Y + Height / 2; }
            set { Shape.Y = value - Height / 2; }
        }

        public EShape Type => Shape.Type;

        public string Title {
            get { return Shape.Title; }
            set { Shape.Title = value; }
        }

        public List<string> Text { get; set; }

        public override string ToString() => Shape.ToString();
    }
}
