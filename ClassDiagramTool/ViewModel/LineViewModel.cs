﻿using ClassDiagramTool.Model;
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
        private const int OFFSET = 20;
        private const int ARROW = 5;

        private ShapeViewModel From { get; }
        private ShapeViewModel To { get; }

        private ConnectionPoint FP => From.Points[FromPoint];
        private ConnectionPoint TP => To.Points[ToPoint];

        public Line Line { get; }

        public List<LinePart> LineParts { get; set; }

        public List<Point> StartLineCap => CalculatePoints(FP);
        public List<Point> EndLineCap   => CalculatePoints(TP);

        #region Wrapper
        public int FromNumber => From.Number;
        public int ToNumber => To.Number;

        public int FromPoint => 0;
        public int ToPoint => 0;

        public ELine Type => Line.Type;

        public string Label {
            get { return Line.Label; }
            set { Line.Label = value; }
        }
        #endregion

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

            CalculateLinePart(FP.Orientation, FP.X, FP.Y, TP.Orientation, TP.X, TP.Y, true);

            OnPropertyChanged(nameof(LineParts));
            OnPropertyChanged(nameof(StartLineCap));
            OnPropertyChanged(nameof(EndLineCap));
        }

        private void CalculateLinePart(EConnectionPoint from, double X1, double Y1, EConnectionPoint to, double X2, double Y2, bool first)
        {
            double nextX = 0, nextY = 0;

                 if  (X1 == X2 && Y1 == Y2) return;
            else if ((X1 == X2 || Y1 == Y2) && !first) { nextX = X2; nextY = Y2; }
            else
            { 
                switch (from)
                {
                    case EConnectionPoint.North:                nextX = X1;
                             if (EConnectionPoint.North == to)  nextY = Math.Min(Y1, Y2) - OFFSET;
                        else if (EConnectionPoint.South == to)  nextY = (Y1 + Y2) / 2;
                        else                                    nextY = Y2;
                        break;
                    case EConnectionPoint.South:                nextX = X1;
                             if (EConnectionPoint.South == to)  nextY = Math.Max(Y1, Y2) + OFFSET;
                        else if (EConnectionPoint.North == to)  nextY = (Y1 + Y2) / 2;
                        else                                    nextY = Y2;
                        break;
                    case EConnectionPoint.East:                 nextY = Y1;
                             if (EConnectionPoint.East == to)   nextX = Math.Max(X1, X2) + OFFSET;
                        else if (EConnectionPoint.West == to)   nextX = (X1 + X2) / 2;
                        else                                    nextX = X2;
                        break;
                    case EConnectionPoint.West:                 nextY = Y1;
                             if (EConnectionPoint.West == to)   nextX = Math.Min(X1, X2) - OFFSET;
                        else if (EConnectionPoint.East == to)   nextX = (X1 + X2) / 2;
                        else                                    nextX = X2;
                        break;
                }

                switch (from)
                {
                    case EConnectionPoint.North:
                    case EConnectionPoint.South:
                        if (X1 < X2) from = EConnectionPoint.East;
                        else         from = EConnectionPoint.West;
                        break;
                    case EConnectionPoint.East :
                    case EConnectionPoint.West :
                        if (Y1 < Y2) from = EConnectionPoint.South;
                        else         from = EConnectionPoint.North;
                        break;
                }
            }

            LineParts.Add(new LinePart() { X1 = X1, Y1 = Y1, X2 = nextX, Y2 = nextY });

            CalculateLinePart(from, nextX, nextY, to, X2, Y2, false);
        }

        private List<Point> CalculatePoints(ConnectionPoint p)
        {
            List<Point> Points = new List<Point>();

            switch(p.Orientation)
            {
                case EConnectionPoint.North:
                    Points.Add(new Point(p.X - ARROW, p.Y - ARROW * 1.5));
                    Points.Add(new Point(p.X        , p.Y              ));
                    Points.Add(new Point(p.X + ARROW, p.Y - ARROW * 1.5));
                    Points.Add(new Point(p.X        , p.Y - ARROW * 3.0));
                    break;
                case EConnectionPoint.South:
                    Points.Add(new Point(p.X - ARROW, p.Y + ARROW * 1.5));
                    Points.Add(new Point(p.X        , p.Y              ));
                    Points.Add(new Point(p.X + ARROW, p.Y + ARROW * 1.5));
                    Points.Add(new Point(p.X        , p.Y + ARROW * 3.0));
                    break;
                case EConnectionPoint.East:
                    Points.Add(new Point(p.X + ARROW * 1.5, p.Y - ARROW));
                    Points.Add(new Point(p.X              , p.Y        ));
                    Points.Add(new Point(p.X + ARROW * 1.5, p.Y + ARROW));
                    Points.Add(new Point(p.X + ARROW * 3.0, p.Y        ));
                    break;
                case EConnectionPoint.West:
                    Points.Add(new Point(p.X - ARROW * 1.5, p.Y - ARROW));
                    Points.Add(new Point(p.X              , p.Y        ));
                    Points.Add(new Point(p.X - ARROW * 1.5, p.Y + ARROW));
                    Points.Add(new Point(p.X - ARROW * 3.0, p.Y        ));
                    break;
            }

            return Points;
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