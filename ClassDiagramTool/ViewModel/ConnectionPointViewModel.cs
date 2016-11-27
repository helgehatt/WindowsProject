using ClassDiagramTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.ViewModel
{
    public class ConnectionPointViewModel : IConnectionPoint
    {
        private ConnectionPoint ConnectionPoint { get; }

        private ShapeViewModel ShapeViewModel { get; }

        public ConnectionPointViewModel(EConnectionPoint orientation, ShapeViewModel shapeViewModel)
            : this(new ConnectionPoint() { Orientation = orientation}, shapeViewModel)
        {
        }

        public ConnectionPointViewModel(ConnectionPoint connectionPoint, ShapeViewModel shapeViewModel)
        {
            ConnectionPoint = connectionPoint;
            ShapeViewModel = shapeViewModel;
        }

        #region Wrapper
        public int Number => ConnectionPoint.Number;

        public Shape Shape => ShapeViewModel.Shape;

        public EConnectionPoint Orientation => ConnectionPoint.Orientation;

        public double Percentile => ConnectionPoint.Percentile;
        
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

    }
}
