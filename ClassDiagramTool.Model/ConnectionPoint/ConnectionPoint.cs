using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Model
{
    [Serializable]
    public class ConnectionPoint : IConnectionPoint
    {
        public static int number;

        public int Number { get; set; } = number++;

        public int OnShape { get; set; }

        public EConnectionPoint Orientation { get; set; }

        public double Percentile { get; set; } = 0.5;
    }
}
