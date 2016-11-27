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
    class AddLineAdorner : Adorner
    {
        VisualCollection VisualChildren;
        protected override int VisualChildrenCount { get { return VisualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return VisualChildren[index]; }

        List<Button> ButtonList = new List<Button>();

        public AddLineAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            ShapeViewModel Shape = (AdornedElement as UserControl).DataContext as ShapeViewModel;

            Style ConnectionPointStyle = FindResource("ConnectionPointStyle") as Style;

            VisualChildren = new VisualCollection(this);

            foreach (ConnectionPoint Point in Shape.Points)
            {
                var Button = new Button() { Style = ConnectionPointStyle };

                double start = 0;

                Thumb.DragStarted += new DragStartedEventHandler((sender, e) =>
                {
                    start = Point.Percentile;
                });

                Thumb.DragDelta += new DragDeltaEventHandler((sender, e) =>
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
                });
                ButtonList.Add(Button);
                VisualChildren.Add(Button);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            ShapeViewModel Shape = (AdornedElement as UserControl).DataContext as ShapeViewModel;

            for (int i = 0; i < ButtonList.Count; i++)
            {
                ConnectionPoint Point = Shape.Points[i];
                ButtonList[i].Arrange(
                    new Rect(
                        Point.X - Shape.X - Shape.Width / 2,
                        Point.Y - Shape.Y - Shape.Height / 2,
                        Shape.Width,
                        Shape.Height));
            }

            return finalSize;
        }
    }
}
