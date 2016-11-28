using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.Model
{
    public interface IConnectionPoint
    {
        int Number { get; }

        int OnShape { get; }

        EConnectionPoint Orientation { get; }

        double Percentile { get; }
    }
}
