using System.Collections.Generic;

namespace AdvancedWPFDemo.Model
{
    public class Shape : IShape
    {
        private static int _number;

        public int Number { get; set; } = _number++;

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public EShape Type { get; set; }

        public List<string> Data { get; set; }

        //public void NewNumber()
        //{
        //    Number = ++counter;
        //}

        public override string ToString() => $"{GetType().Name} ({Number})";

     
    }
}
