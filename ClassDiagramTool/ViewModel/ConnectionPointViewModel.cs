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

        public ConnectionPoint ConnectionPoint { get; }
        
        public ShapeViewModel ShapeViewModel { get; }

        public List<LineViewModel> LineViewModels { get; set; } = new List<LineViewModel>();
        
        public string Visibility => MainViewModel.IsAddingLine ? "Visible" : "Hidden";
        public void NotifyVisibility() { OnPropertyChanged(nameof(Visibility)); }
        #endregion

        public RelayCommand<MouseButtonEventArgs> MoveConnectionPointCommand => new RelayCommand<MouseButtonEventArgs>((e) => UndoRedoController.Execute(new MoveConnectionPointCommand(this, e)));
        public RelayCommand<MouseButtonEventArgs> AddLineCommand => new RelayCommand<MouseButtonEventArgs>(MainViewModel.ObjectCommands.AddLine, e => e.Source is UserControl && MainViewModel.IsAddingLine);

        public ConnectionPointViewModel(ConnectionPoint connectionPoint, ShapeViewModel shapeViewModel)
        {
            ConnectionPoint = connectionPoint;
            ShapeViewModel = shapeViewModel;
        }

        #region Wrapper
        public int Number => ConnectionPoint.Number;

        public Shape Shape => ShapeViewModel.Shape;

        public EConnectionPoint Orientation => ConnectionPoint.Orientation;

        public double Percentile {
            get { return ConnectionPoint.Percentile; }
            set { ConnectionPoint.Percentile = value; }
        }
        
        public double X
        {
            get
            {
                switch (Orientation)
                {
                    case EConnectionPoint.North: return Shape.X + Shape.Width * Percentile;
                    case EConnectionPoint.South: return Shape.X + Shape.Width * Percentile;
                    case EConnectionPoint.East : return Shape.X + Shape.Width             ;
                    default                    : return Shape.X + 0                       ;
                }
            }
        }

        public double Y
        {
            get
            {
                switch (Orientation)
                {
                    case EConnectionPoint.South: return Shape.Y + Shape.Height             ;
                    case EConnectionPoint.East : return Shape.Y + Shape.Height * Percentile;
                    case EConnectionPoint.West : return Shape.Y + Shape.Height * Percentile;
                    default                    : return Shape.Y + 0                        ;
                }
            }
        }
        #endregion

        public double Left => X - 6;
        public double Top  => Y - 6;

        public void Update()
        {
            OnPropertyChanged(nameof(Left));
            OnPropertyChanged(nameof(Top));
        }
    }
}
