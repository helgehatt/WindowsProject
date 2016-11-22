using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        public readonly Line Line;

        public ShapeViewModel From { get; set; }
        public ShapeViewModel To { get; set; }

        public int FromNumber => From.Number;
        public int ToNumber => To.Number;

        public int FromPoint => 1;
        public int ToPoint => 3;

        public ELine Type => Line.Type;

        public string Label {
            get { return Line.Label; }
            set { Line.Label = value; }
        }

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to)
        {
            Line = line;
            From = from;
            To = to;
        }

        protected LineViewModel(Line line, ConnectionPoint from, ConnectionPoint to)
        {
            Line = line;
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