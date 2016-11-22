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

        public ShapeViewModel From { get; set; }
        public ShapeViewModel To { get; set; }

        //public ConnectionPoint From { get; set; }
        //public ConnectionPoint To { get; set; }

        public int FromNumber => From.Shape.Number;
        public int ToNumber => To.Shape.Number;

        public int FromPoint => From.Number;
        public int ToPoint => To.Number;

        public ELine Type => Line.Type;

        public string Label {
            get { return Line.Label; }
            set { Line.Label = value; }
        }

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to)
        {
            From = from;
            To = to;
        }

        protected LineViewModel(Line line, ConnectionPoint from, ConnectionPoint to)
        {
            //From = from;
            //To = to;
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