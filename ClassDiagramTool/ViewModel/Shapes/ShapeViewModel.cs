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

        protected ShapeViewModel(Shape shape) {
            this.shape = shape;
            Width = 250;
            Height = 100;
        }

        public int Number => shape.Number;

        public double X {
            get { return shape.X; }
            set { shape.X = value; }
        }

        public double Y {
            get { return shape.Y; }
            set { shape.Y = value; }
        }

        public double Width {
            get { return shape.Width; }
            set { shape.Width = value; }
        }

        public double Height {
            get { return shape.Height; }
            set { shape.Height = value; }
        }

        public double CenterX {
            get { return X + Width / 2; }
            set { shape.X = value - Width / 2; }
        }

        public double CenterY {
            get { return Y + Height / 2; }
            set { shape.Y = value - Height / 2; }
        }

        public EShape Type => shape.Type;

        public string Title {
            get { return shape.Title; }
            set { shape.Title = value; }
        }

        public List<string> Text { get; set; }

        public override string ToString() => shape.ToString();
    }
}
