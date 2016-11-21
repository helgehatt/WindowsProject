using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Model
{
    public class ConnectionPoint
    {
        public Shape Shape;
        private EConnectionPoint Orientation;
        private double Percentile;

        public ConnectionPoint(Shape shape, EConnectionPoint orientation)
            : this(shape, orientation, 0.5)
        {
        }

        public ConnectionPoint(Shape shape, EConnectionPoint orientation, double percentile)
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
                switch(Orientation)
                {
                    case EConnectionPoint.South: return Shape.Y + Shape.Height             ;
                    case EConnectionPoint.East : return Shape.Y + Shape.Height * Percentile;
                    case EConnectionPoint.West : return Shape.Y + Shape.Height * Percentile;
                    default                    : return Shape.Y + 0                        ;
                }
            }
        }
    }
}
