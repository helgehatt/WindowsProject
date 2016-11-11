using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDiagramTool.ViewModel.Shapes
{

    public class ConnectionPoint
    {
        private ShapeViewModel Shape;
        private EConnectionPoint Orientation;
        private double Percentile;

        public ConnectionPoint(ShapeViewModel shape, EConnectionPoint orientation)
        {
            Shape = shape;
            Orientation = orientation;
            Percentile = 0.5;
        }

        public double X
        {
            get
            {
                switch(Orientation)
                {
                    case EConnectionPoint.North: return Shape.X + Shape.Width * Percentile;
                    case EConnectionPoint.South: return Shape.X + Shape.Width * Percentile;
                    case EConnectionPoint.East : return Shape.X + Shape.Width             ;
                    case EConnectionPoint.West : return Shape.X                           ;
                    default                    : return 0                                 ;
                }
            }
        }

        public double Y
        {
            get
            {
                switch(Orientation)
                {
                    case EConnectionPoint.North: return Shape.Y                            ;
                    case EConnectionPoint.South: return Shape.Y + Shape.Height             ;
                    case EConnectionPoint.East : return Shape.Y + Shape.Height * Percentile;
                    case EConnectionPoint.West : return Shape.Y + Shape.Height * Percentile;
                    default                    : return 0                                  ;
                }
            }
        }

        public enum EConnectionPoint
        {
            North,
            South,
            East,
            West
        }
    }

}
