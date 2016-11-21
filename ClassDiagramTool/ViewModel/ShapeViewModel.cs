using System;
using System.Collections.Generic;
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

        public RelayCommand<MouseButtonEventArgs> MoveShapeCommand => new RelayCommand<MouseButtonEventArgs>((e) => UndoRedoController.AddAndExecute(new MoveShape(this, e)), e => !MainViewModel.IsAddingLine);
        public RelayCommand<MouseButtonEventArgs> EditTextCommand => new RelayCommand<MouseButtonEventArgs>((e) => UndoRedoController.AddAndExecute(new EditText(e)), e => e.Source is TextBox);

        public Shape Shape { get; }

        private bool selected = false;
        public bool Selected {
            get { return selected; }
            set { selected = value; OnPropertyChanged(); }
        }
        private bool dragging = false;
        public bool Dragging
        {
            get { return dragging; }
            set { dragging = value; OnPropertyChanged(); }
        }

        protected ShapeViewModel(Shape shape) {
            Shape = shape;
            Width = 250;
            Height = 100;
            Title = "Title";
            Text = new List<string>() { "text1", "text2" };

            //P = new List<ConnectionPoint>()
            //{
            //    new ConnectionPoint(this, EConnectionPoint.North),
            //    new ConnectionPoint(this, EConnectionPoint.South),
            //    new ConnectionPoint(this, EConnectionPoint.East ),
            //    new ConnectionPoint(this, EConnectionPoint.West )
            //};
        }
        
        public List<ConnectionPoint> Points { get; set; }

        public int Number => Shape.Number;

        public double X {
            get { return Shape.X; }
            set { Shape.X = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(P));
            }
        }

        public double Y {
            get { return Shape.Y; }
            set { Shape.Y = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(P));
            }
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

        public double PointX(ConnectionPoint point)
        {
            switch(point.Orientation)
            {
                case EConnectionPoint.North: return Shape.X + Shape.Width * point.Percentile;
                case EConnectionPoint.South: return Shape.X + Shape.Width * point.Percentile;
                case EConnectionPoint.East : return Shape.X + Shape.Width                   ;
                default          : return Shape.X + 0                             ;
            }
        }

        public double PointY(ConnectionPoint point)
        {
            switch(point.Orientation)
            {
                case EConnectionPoint.South: return Shape.Y + Shape.Height                   ;
                case EConnectionPoint.East : return Shape.Y + Shape.Height * point.Percentile;
                case EConnectionPoint.West : return Shape.Y + Shape.Height * point.Percentile;
                default          : return Shape.Y + 0                              ;
            }
        }
    }
}
