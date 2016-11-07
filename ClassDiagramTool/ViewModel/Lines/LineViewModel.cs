﻿using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        private readonly Line Line;

        public ShapeViewModel To { get; set; }
        public ShapeViewModel From { get; set; }

        public double X { get; } // Unused
        public double Y { get; } // Unused

        public int FromNumber => From.Number;
        public int ToNumber => To.Number;
        public string Label { get; set; }
        public ELine Type { get; set; }

        protected LineViewModel(Line line)
        {
            Line = line;
        }

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to) 
            : this(line)
        {
            To = to;
            From = from;
        }
    }
}