using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace ClassDiagramTool.ViewModel
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        #region Fields
        private const int OFFSET = 20;
        private const int ARROW = 5;

        public ConnectionPointViewModel From { get; set; }
        public ConnectionPointViewModel To { get; set; }

        public Line Line { get; }

        public List<LinePart> LineParts { get; set; }

        public List<Point> StartLineCap => CalculateLineCapPoints(From);
        public List<Point> EndLineCap   => CalculateLineCapPoints(To);
        #endregion

        public LineViewModel(Line line, ConnectionPointViewModel from, ConnectionPointViewModel to)
        {
            Line = line;
            From = from;
            To   = to;

            From.LineViewModels.Add(this);
            To.LineViewModels.Add(this);

            FromShape = From.ShapeViewModel.Number;
            ToShape   = To.ShapeViewModel.Number;

            FromPoint = From.Number;
            ToPoint   = To.Number;

            CalculateLinePart();
        }

        #region Wrapper
        public int FromShape
        {
            get { return Line.FromShape; }
            set { Line.FromShape = value; }
        }

        public int ToShape
        {
            get { return Line.ToShape; }
            set { Line.ToShape = value; }
        }

        public int FromPoint
        {
            get { return Line.FromPoint; }
            set { Line.FromPoint = value; }
        }
        public int ToPoint
        {
            get { return Line.ToPoint; }
            set { Line.ToPoint = value; }
        }

        public ELine Type => Line.Type;

        public string Label {
            get { return Line.Label; }
            set { Line.Label = value; }
        }
        #endregion

        #region Calculations
        public void CalculateLinePart()
        {
            LineParts = new List<LinePart>();

            Point FromP = GetOffsetPoint(From.Orientation, From.X, From.Y);
            Point ToP = GetOffsetPoint(To.Orientation, To.X, To.Y);
            
            LineParts.Add(new LinePart() { X1 = From.X, Y1 = From.Y, X2 = FromP.X, Y2 = FromP.Y });
            LineParts.Add(new LinePart() { X1 = To.X, Y1 = To.Y, X2 = ToP.X, Y2 = ToP.Y });
            
            CalculateLinePart(From.Orientation, FromP.X, FromP.Y, To.Orientation, ToP.X, ToP.Y, true);

            //CalculateLinePart(From.Orientation, From.X, From.Y, To.Orientation, To.X, To.Y, true);
            
            OnPropertyChanged(nameof(LineParts));
            OnPropertyChanged(nameof(StartLineCap));
            OnPropertyChanged(nameof(EndLineCap));
        }

        private Point GetOffsetPoint(EConnectionPoint orientation, double X, double Y)
        {
            switch (orientation)
            {
                case EConnectionPoint.North: return new Point(X, Y - OFFSET);
                case EConnectionPoint.South: return new Point(X, Y + OFFSET);
                case EConnectionPoint.East : return new Point(X + OFFSET, Y);
                case EConnectionPoint.West : return new Point(X - OFFSET, Y);
            }
            return new Point(0, 0);
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
                    case EConnectionPoint.North:
                        nextX = X1;
                        switch (to)
                        {
                            case EConnectionPoint.North:
                                nextY = Math.Min(Y1, Y2);
                                break;
                            case EConnectionPoint.South:
                                if (From.ShapeViewModel.Y - OFFSET > To.ShapeViewModel.Y + To.ShapeViewModel.Height + OFFSET)
                                    nextY = (From.ShapeViewModel.Y + To.ShapeViewModel.Y + To.ShapeViewModel.Height) / 2;
                                else
                                    nextY = Math.Min(From.ShapeViewModel.Y - OFFSET, Y2);
                                break;
                            case EConnectionPoint.East:
                                     if (X2 < X1) nextY = Math.Min(Y1, Y2);
                                else if (Y1 < Y2) nextY = Math.Min(Y1, To.ShapeViewModel.Y - OFFSET);
                                else if (From.ShapeViewModel.Y - OFFSET < To.ShapeViewModel.Y + To.ShapeViewModel.Height + OFFSET)
                                                  nextY = To.ShapeViewModel.Y - OFFSET;
                                else              nextY = (From.ShapeViewModel.Y + To.ShapeViewModel.Y + To.ShapeViewModel.Height) / 2;
                                break;
                            case EConnectionPoint.West:
                                     if (X1 < X2) nextY = Math.Min(Y1, Y2);
                                else if (Y1 < Y2) nextY = Math.Min(Y1, To.ShapeViewModel.Y - OFFSET);
                                else if (From.ShapeViewModel.Y - OFFSET < To.ShapeViewModel.Y + To.ShapeViewModel.Height + OFFSET)
                                                  nextY = To.ShapeViewModel.Y - OFFSET;
                                else              nextY = (From.ShapeViewModel.Y + To.ShapeViewModel.Y + To.ShapeViewModel.Height) / 2;
                                break;
                        }
                        break;
                    case EConnectionPoint.South:
                        nextX = X1;
                        switch (to)
                        {
                            case EConnectionPoint.North:
                                if (From.ShapeViewModel.Y + From.ShapeViewModel.Height + OFFSET < To.ShapeViewModel.Y - OFFSET)
                                    nextY = (From.ShapeViewModel.Y + From.ShapeViewModel.Height + To.ShapeViewModel.Y) / 2;
                                else
                                    nextY = Math.Max(From.ShapeViewModel.Y + From.ShapeViewModel.Height + OFFSET, Y2);
                                break;
                            case EConnectionPoint.South:
                                nextY = Math.Max(Y1, Y2);
                                break;
                            case EConnectionPoint.East:
                                     if (X2 < X1) nextY = Math.Max(Y1, Y2);
                                else if (Y2 < Y1) nextY = Math.Max(Y1, To.ShapeViewModel.Y + To.ShapeViewModel.Height + OFFSET);
                                else if (From.ShapeViewModel.Y + From.ShapeViewModel.Height + OFFSET > To.ShapeViewModel.Y - OFFSET)
                                                  nextY = To.ShapeViewModel.Y + To.ShapeViewModel.Height + OFFSET;
                                else              nextY = (From.ShapeViewModel.Y + From.ShapeViewModel.Height + To.ShapeViewModel.Y) / 2;
                                break;
                            case EConnectionPoint.West:
                                     if (X1 < X2) nextY = Math.Max(Y1, Y2);
                                else if (Y2 < Y1) nextY = Math.Max(Y1, To.ShapeViewModel.Y + To.ShapeViewModel.Height + OFFSET);
                                else if (From.ShapeViewModel.Y + From.ShapeViewModel.Height + OFFSET > To.ShapeViewModel.Y - OFFSET)
                                                  nextY = To.ShapeViewModel.Y + To.ShapeViewModel.Height + OFFSET;
                                else              nextY = (From.ShapeViewModel.Y + From.ShapeViewModel.Height + To.ShapeViewModel.Y) / 2;
                                break;
                        }
                        break;
                    case EConnectionPoint.East:
                        nextY = Y1;
                        switch (to)
                        {
                            case EConnectionPoint.North:
                                     if (Y1 < Y2) nextX = Math.Max(X1, X2);
                                else if (X2 < X1) nextX = Math.Max(X1, To.ShapeViewModel.X + To.ShapeViewModel.Width + OFFSET);
                                else if (From.ShapeViewModel.X + From.ShapeViewModel.Height + OFFSET > To.ShapeViewModel.X - OFFSET)
                                                  nextX = To.ShapeViewModel.X + To.ShapeViewModel.Width + OFFSET;
                                else              nextX = (From.ShapeViewModel.X + From.ShapeViewModel.Width + To.ShapeViewModel.X) / 2;
                                break;
                            case EConnectionPoint.South:
                                     if (Y2 < Y1) nextX = Math.Max(X1, X2);
                                else if (X2 < X1) nextX = Math.Max(X1, To.ShapeViewModel.X + To.ShapeViewModel.Width + OFFSET);
                                else if (From.ShapeViewModel.X + From.ShapeViewModel.Width + OFFSET > To.ShapeViewModel.X - OFFSET)
                                                  nextX = To.ShapeViewModel.X + To.ShapeViewModel.Width + OFFSET;
                                else              nextX = (From.ShapeViewModel.X + From.ShapeViewModel.Width + To.ShapeViewModel.X) / 2;
                                break;
                            case EConnectionPoint.East:
                                nextX = Math.Max(X1, X2);
                                break;
                            case EConnectionPoint.West:
                                if (From.ShapeViewModel.X + From.ShapeViewModel.Width + OFFSET < To.ShapeViewModel.X - OFFSET)
                                    nextX = (From.ShapeViewModel.X + From.ShapeViewModel.Width + To.ShapeViewModel.X) / 2;
                                else
                                    nextX = Math.Max(From.ShapeViewModel.X + From.ShapeViewModel.Width + OFFSET, X2);
                                break;
                        }
                        break;
                    case EConnectionPoint.West:
                        nextY = Y1;
                        switch (to)
                        {
                            case EConnectionPoint.North:
                                     if (Y1 < Y2) nextX = Math.Min(X1, X2);
                                else if (X1 < X2) nextX = Math.Min(X1, To.ShapeViewModel.X - OFFSET);
                                else if (From.ShapeViewModel.X - OFFSET < To.ShapeViewModel.X + To.ShapeViewModel.Width + OFFSET)
                                                  nextX = To.ShapeViewModel.X - OFFSET;
                                else              nextX = (From.ShapeViewModel.X + To.ShapeViewModel.X + To.ShapeViewModel.Width) / 2;
                                break;
                            case EConnectionPoint.South:
                                     if (Y2 < Y1) nextX = Math.Min(X1, X2);
                                else if (X1 < X2) nextX = Math.Min(X1, To.ShapeViewModel.X - OFFSET);
                                else if (From.ShapeViewModel.X - OFFSET < To.ShapeViewModel.X + To.ShapeViewModel.Width + OFFSET)
                                                  nextX = To.ShapeViewModel.X - OFFSET;
                                else              nextX = (From.ShapeViewModel.X + To.ShapeViewModel.X + To.ShapeViewModel.Width) / 2;
                                break;
                            case EConnectionPoint.East:
                                if (From.ShapeViewModel.X - OFFSET > To.ShapeViewModel.X + To.ShapeViewModel.Width + OFFSET)
                                    nextX = (From.ShapeViewModel.X + To.ShapeViewModel.X + To.ShapeViewModel.Width) / 2;
                                else
                                    nextX = Math.Min(From.ShapeViewModel.X - OFFSET, X2);
                                break;
                            case EConnectionPoint.West:
                                nextX = Math.Min(X1, X2);
                                break;
                        }
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

        private List<Point> CalculateLineCapPoints(ConnectionPointViewModel p)
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
        #endregion
    }

    public class LinePart
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
    }
}