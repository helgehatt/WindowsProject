using ClassDiagramTool.Commands.UndoRedoCommands;
using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.ViewModel
{
    public class ConnectionPointViewModel : BaseViewModel, IConnectionPoint
    {
        #region Fields
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;

        public List<LineViewModel> LineViewModels { get; set; } = new List<LineViewModel>();

        private string _visibility = "Hidden";
        public string Visibility
        {
            get { return _visibility; }
            set {
                _visibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ConnectionPointViewModel(EConnectionPoint orientation, ShapeViewModel shapeViewModel)
            : this(new ConnectionPoint() { Orientation = orientation}, shapeViewModel)
        {
        }

        public ConnectionPointViewModel(ConnectionPoint connectionPoint, ShapeViewModel shapeViewModel)
        {
            ConnectionPoint = connectionPoint;
            ShapeViewModel = shapeViewModel;

            ShapeViewModel.ConnectionPointViewModels.Add(this);

            OnShape = ShapeViewModel.Number;
        }

        #region Wrapper
        public ConnectionPoint ConnectionPoint { get; }
        public ShapeViewModel ShapeViewModel { get; set; }

        public int Number
        {
            get { return ConnectionPoint.Number; }
            set { ConnectionPoint.Number = value; }
        }

        public int OnShape
        {
            get { return ConnectionPoint.OnShape; }
            set { ConnectionPoint.OnShape = value; }
        }

        public EConnectionPoint Orientation => ConnectionPoint.Orientation;

        public double Percentile
        {
            get { return ConnectionPoint.Percentile; }
            set { ConnectionPoint.Percentile = value; }
        }
        #endregion

        public double X
        {
            get
            {
                switch (Orientation)
                {
                    case EConnectionPoint.North: return ShapeViewModel.X + ShapeViewModel.Width * Percentile;
                    case EConnectionPoint.South: return ShapeViewModel.X + ShapeViewModel.Width * Percentile;
                    case EConnectionPoint.East : return ShapeViewModel.X + ShapeViewModel.Width             ;
                    default                    : return ShapeViewModel.X + 0                                ;
                }
            }
        }

        public double Y
        {
            get
            {
                switch (Orientation)
                {
                    case EConnectionPoint.South: return ShapeViewModel.Y + ShapeViewModel.Height             ;
                    case EConnectionPoint.East : return ShapeViewModel.Y + ShapeViewModel.Height * Percentile;
                    case EConnectionPoint.West : return ShapeViewModel.Y + ShapeViewModel.Height * Percentile;
                    default                    : return ShapeViewModel.Y + 0                                 ;
                }
            }
        }

        public double Left => X - 6;
        public double Top  => Y - 6;

        public void NotifyPosition()
        {
            OnPropertyChanged(nameof(Left));
            OnPropertyChanged(nameof(Top));
        }
    }
}
