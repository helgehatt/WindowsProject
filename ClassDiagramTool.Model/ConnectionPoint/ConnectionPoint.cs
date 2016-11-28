﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Model
{
    public class ConnectionPoint : IConnectionPoint
    {
        private static int number;

        public int Number { get; } = number++;

        public EConnectionPoint Orientation { get; set; }

        public double Percentile { get; set; } = 0.5;
    }
}
