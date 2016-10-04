using System.Collections.Generic;

namespace WindowsProject.Model
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