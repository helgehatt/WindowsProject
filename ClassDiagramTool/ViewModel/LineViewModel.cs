using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Diagnostics;
using System.Windows;
using static ClassDiagramTool.ViewModel.Shapes.ConnectionPoint;

namespace ClassDiagramTool.ViewModel.Lines
{
    public abstract class LineViewModel : BaseViewModel, ILine
    {
        private readonly Line Line;

        public ShapeViewModel From { get; set; }
        public ShapeViewModel To { get; set; }

        public int FromNumber => From.Number;
        public int ToNumber => To.Number;
        public string Label { get; set; }
        public ELine Type => Line.Type;

        protected LineViewModel(Line line, ShapeViewModel from, ShapeViewModel to)
        {
            Line = line;
            Label = Enum.GetName(typeof(ELine), Type); // Temporary
            To = to;
            From = from;
        }
    }
}