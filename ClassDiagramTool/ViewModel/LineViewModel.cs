using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        public Line Line { get; }

        public List<LinePart> LineParts { get; set; }

        private ShapeViewModel From { get; }
        private ShapeViewModel To { get; }

        private ConnectionPoint FP => From.Points[FromPoint];
        private ConnectionPoint TP => To.Points[ToPoint];

        public int FromNumber => From.Number;
        public int ToNumber => To.Number;

        public int FromPoint => 0;
        public int ToPoint => 0;

        public ELine Type => Line.Type;

        public string Label {
            get { return Line.Label; }
            set { Line.Label = value; }
        }

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to)
        {
            from.Lines.Add(this);
            to  .Lines.Add(this);

            Line = line;
            From = from;
            To = to;

            CalculateLinePart();
        }

        public void CalculateLinePart()
        {
            LineParts = new List<LinePart>();

            CalculateLinePart(FP.Orientation, FP.X, FP.Y, TP.Orientation, TP.X, TP.Y);

            OnPropertyChanged(nameof(LineParts));
        }

        private void CalculateLinePart(EConnectionPoint from, double X1, double Y1, EConnectionPoint to, double X2, double Y2)
        {
            double nextX = 0, nextY = 0;

                 if (X1 == X2 && Y1 == Y2) return;
            else if (X1 == X2 || Y1 == Y2) { nextX = X2; nextY = Y2; }
            else
            { 
                switch (from)
                {
                    case EConnectionPoint.North:    nextX = X1;
                        if (Y1 < Y2)                nextY = Y1 - 10;
                        else                        nextY = Y2;
                        break;
                    case EConnectionPoint.South:    nextX = X1;
                        if (Y1 < Y2)                nextY = Y2;
                        else                        nextY = Y1 + 10;
                        break;
                    case EConnectionPoint.East:     nextY = Y1;
                        if (X1 < X2)                nextX = X2;
                        else                        nextX = X1 + 10;
                        break;
                    case EConnectionPoint.West:     nextY = Y1;
                        if (X1 < X2)                nextX = X1 - 10;
                        else                        nextX = X2;
                        break;
                }

                switch (from)
                {
                    case EConnectionPoint.North:
                    case EConnectionPoint.South:
                        if (X1 < X2) from = EConnectionPoint.East;
                        else         from = EConnectionPoint.West;
                             if      (to == EConnectionPoint.North) nextY -= 10;
                        else if      (to == EConnectionPoint.South) nextY += 10;
                        break;
                    case EConnectionPoint.East :
                    case EConnectionPoint.West :
                        if (Y1 < Y2) from = EConnectionPoint.South;
                        else         from = EConnectionPoint.North;
                             if      (to == EConnectionPoint.West) nextX -= 10;
                        else if      (to == EConnectionPoint.East) nextX += 10;
                        break;
                }
            }

            LineParts.Add(new LinePart() { X1 = X1, Y1 = Y1, X2 = nextX, Y2 = nextY });

            CalculateLinePart(from, nextX, nextY, to, X2, Y2);
        }
    }

    public class LinePart
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
    }
}