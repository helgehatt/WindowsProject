using System.Collections.Generic;

namespace AdvancedWPFDemo.Model
{
    public interface IShape
    {
        List<string> Data { get; set; }
        double Height { get; set; }
        int Number { get; }
        EShape Type { get; }
        double Width { get;}
        double X { get; }
        double Y { get; }

        string ToString();
    }
}