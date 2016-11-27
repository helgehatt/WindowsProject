﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Model
{
    [Serializable]
    public class ConnectionPoint : IConnectionPoint
    {
        public int Number { get; set; }

        [NonSerialized]
        private Shape shape;

        public Shape Shape
        {
            get { return shape; }
            set { shape = value; }
        }

        public EConnectionPoint Orientation { get; set; }
        public double Percentile { get; set; } = 0.5;

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
    }
}
