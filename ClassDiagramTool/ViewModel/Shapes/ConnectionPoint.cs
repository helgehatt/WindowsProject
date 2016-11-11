using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDiagramTool.ViewModel.Shapes
{
    public enum EConnectionPoint
    {
        North,
        South,
        East,
        West
    }

    public class ConnectionPoint
    {
        private ShapeViewModel Shape;
        private EConnectionPoint Orientation;
        private double Percentile;

        public ConnectionPoint(ShapeViewModel shape, EConnectionPoint orientation)
            : this(shape, orientation, 0.5)
        {
        }

        public ConnectionPoint(ShapeViewModel shape, EConnectionPoint orientation, double percentile)
        {
            Shape = shape;
            Orientation = orientation;
            Percentile = percentile;
        }

        public double X
        {
            get
            {
                switch(Orientation)
                {
                    case EConnectionPoint.North: return Shape.Width * Percentile;
                    case EConnectionPoint.South: return Shape.Width * Percentile;
                    case EConnectionPoint.East : return Shape.Width             ;
                    default                    : return 0                       ;
                }
            }
        }

        public double Y
        {
            get
            {
                switch(Orientation)
                {
                    case EConnectionPoint.South: return Shape.Height             ;
                    case EConnectionPoint.East : return Shape.Height * Percentile;
                    case EConnectionPoint.West : return Shape.Height * Percentile;
                    default                    : return 0                        ;
                }
            }
        }
    }
}
