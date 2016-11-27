using ClassDiagramTool.Model;
using ClassDiagramTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ClassDiagramTool.View.Adorners
{
    class ConnectionPointAdorner : Adorner
    {
        VisualCollection VisualChildren;
        protected override int VisualChildrenCount { get { return VisualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return VisualChildren[index]; }

        List<Thumb> ThumbList = new List<Thumb>();

        public ConnectionPointAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            ShapeViewModel Shape = (AdornedElement as UserControl).DataContext as ShapeViewModel;

            Style CustomThumbStyle = (Style)FindResource("ThumbStyle");

            VisualChildren = new VisualCollection(this);
            foreach (ConnectionPoint Point in Shape.Points)
            {
                Thumb NewThumb = new Thumb()
                {
                    Height = 10,
                    Width = 10,
                    Style = CustomThumbStyle
                };
                double start = 0;
                NewThumb.DragStarted += new DragStartedEventHandler((sender, e) =>
                {
                    start = Point.Percentile;
                });
                NewThumb.DragDelta += new DragDeltaEventHandler((sender, e) =>
                {
                    switch (Point.Orientation)
                    {
                        case EConnectionPoint.North:
                        case EConnectionPoint.South:
                            Point.Percentile = Math.Min(Math.Max(start + e.HorizontalChange / Shape.Width, 0.0), 1.0);
                            break;
                        case EConnectionPoint.East:
                        case EConnectionPoint.West:
                            Point.Percentile = Math.Min(Math.Max(start + e.VerticalChange / Shape.Height, 0.0), 1.0);
                            break;
                    }
                    Debug.WriteLine(Point.Percentile);
                });
                NewThumb.MouseEnter += new MouseEventHandler((sender, e) => {
                    NewThumb.Resources["Hover"] = 1.0;
                });
                NewThumb.MouseLeave += new MouseEventHandler((sender, e) => {
                    NewThumb.Resources["Hover"] = 0.0;
                });
                ThumbList.Add(NewThumb);
                VisualChildren.Add(NewThumb);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            ShapeViewModel Shape = (AdornedElement as UserControl).DataContext as ShapeViewModel;
            
            for (int i = 0; i < ThumbList.Count; i++)
            {
                ConnectionPoint Point = Shape.Points[i];
                ThumbList[i].Arrange(
                    new Rect(
                        Point.X - Shape.X - Shape.Width / 2,
                        Point.Y - Shape.Y - Shape.Height / 2,
                        Shape.Width,
                        Shape.Height ));
            }

            return finalSize;
        }
    }
}
