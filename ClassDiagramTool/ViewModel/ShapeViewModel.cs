using System.Collections.Generic;
using System.Windows.Input;
using ClassDiagramTool.Model;
using GalaSoft.MvvmLight.CommandWpf;
using ClassDiagramTool.Commands;
using System.Windows.Controls;
using ClassDiagramTool.Tools;

namespace ClassDiagramTool.ViewModel
{
    public abstract class ShapeViewModel : BaseViewModel, IShape
    {
        #region Fields
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        private bool selected = false;
        private bool dragging = false;

        public Shape Shape { get; }
        public List<LineViewModel> LineViewModels { get; set; }
        #endregion

        #region Commands
        public RelayCommand<MouseButtonEventArgs> MoveShapeCommand => new RelayCommand<MouseButtonEventArgs>((e) => UndoRedoController.Execute(new MoveShapeCommand(this, e)));
        public RelayCommand<MouseButtonEventArgs> EditTextCommand  => new RelayCommand<MouseButtonEventArgs>((e) => UndoRedoController.Execute(new EditTextCommand(e)), e => e.Source is TextBox);
        #endregion


        protected ShapeViewModel(Shape shape)
        {
            Shape = shape;

            // Update Shape references
            foreach (ConnectionPoint point in Points)
            {
                point.Shape = Shape;
            }

            LineViewModels = new List<LineViewModel>();
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value;
                OnPropertyChanged();
            }
        }

        public bool Dragging
        {
            get { return dragging; }
            set { dragging = value;
                OnPropertyChanged();
            }
        }

        #region Wrapper

        public int Number => Shape.Number;

        public double X {
            get { return Shape.X; }
            set { Shape.X = value;
                OnPropertyChanged();
                foreach (LineViewModel line in LineViewModels)
                    line.CalculateLinePart();
            }
        }

        public double Y {
            get { return Shape.Y; }
            set { Shape.Y = value;
                OnPropertyChanged();
            }
        }

        public double Width {
            get { return Shape.Width; }
            set { Shape.Width = value;
                OnPropertyChanged();
            }
        }

        public double Height {
            get { return Shape.Height; }
            set { Shape.Height = value;
                OnPropertyChanged();
            }
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
        
        public List<TextItem> Text {
            get { return Shape.Text; }
            set { Shape.Text = value; }
        }

        public List<ConnectionPoint> Points
        {
            get { return Shape.Points; }
            set { Shape.Points = value; }
        }
        #endregion
    }
}
