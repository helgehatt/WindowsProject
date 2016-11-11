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
        NorthEast,
        NorthWest,
        South,
        SouthEast,
        SouthWest,
        East,
        EastNorth,
        EastSouth,
        West,
        WestNorth,
        WestSouth
    }

    class ConnectionPoint
    {
        public static Point Point(EConnectionPoint p, double x, double y, double width, double height)
        {
            switch(p)
            {
                case EConnectionPoint.North    : return new Point(x +     width / 2, y             );
                case EConnectionPoint.NorthEast: return new Point(x + 3 * width / 4, y             );
                case EConnectionPoint.NorthWest: return new Point(x +     width / 4, y             );
                case EConnectionPoint.South    : return new Point(x +     width / 2, y + height    );
                case EConnectionPoint.SouthEast: return new Point(x + 3 * width / 4, y + height    );
                case EConnectionPoint.SouthWest: return new Point(x +     width / 4, y + height    );
                case EConnectionPoint.East     : return new Point(x +     width,     y + height / 2);
                case EConnectionPoint.EastNorth: return new Point(x +     width,     y + height / 4);
                case EConnectionPoint.EastSouth: return new Point(x +     width, 3 * y + height / 4);
                case EConnectionPoint.West     : return new Point(x            ,     y + height / 2);
                case EConnectionPoint.WestNorth: return new Point(x            ,     y + height / 4);
                case EConnectionPoint.WestSouth: return new Point(x            , 3 * y + height / 4);
                default:                         return new Point();
            }
        }
    }

}
