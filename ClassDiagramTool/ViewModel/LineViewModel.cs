using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Diagnostics;
using System.Windows;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        public readonly Line Line;

        //public ShapeViewModel From { get; set; }
        //public ShapeViewModel To { get; set; }

        public ConnectionPoint From { get; set; }
        public ConnectionPoint To { get; set; }

        public int FromNumber => From.Shape.Number;
        public int ToNumber => To.Shape.Number;
        
        public int FromPoint {
            get { return Line.FromPoint; }
            set { Line.FromPoint = value; }
        }

        public int ToPoint {
            get { return Line.ToPoint; }
            set { Line.ToPoint = value; }
        }

        public string Label {
            get { return Line.Label; }
            set { Line.Label = value; }
        }

        public ELine Type => Line.Type;

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to)
        {
            //Line = line;
            //To = to;
            //From = from;
            //FromPoint = 2;
            //ToPoint = 0;
        }

        protected LineViewModel(Line line, ConnectionPoint from, ConnectionPoint to)
        {
            From = from;
            To = to;
        }

        //protected LineViewModel(Line line, ConnectionPoint from, ConnectionPoint to)
        //{
        //    Line = line;
        //    From = from.Shape;
        //    To = to.Shape;
        //
        //}
    }
}