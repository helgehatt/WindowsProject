using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ClassDiagramTool.ViewModel.Adorners
{
    class ResizeAdorner : Adorner
    {
        VisualCollection visualChildren;
        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }

        Thumb 
            topLeft = new Thumb(),
            topRight = new Thumb(),
            bottomLeft = new Thumb(),
            bottomRight = new Thumb(),
            top = new Thumb(),
            bottom = new Thumb(),
            left = new Thumb(),
            right = new Thumb();

        public ResizeAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);
            
            ConstructThumb(topLeft, Cursors.SizeNWSE);
            ConstructThumb(topRight, Cursors.SizeNESW);
            ConstructThumb(bottomLeft, Cursors.SizeNESW);
            ConstructThumb(bottomRight, Cursors.SizeNWSE);
            ConstructThumb(top, Cursors.SizeNS);
            ConstructThumb(bottom, Cursors.SizeNS);
            ConstructThumb(left, Cursors.SizeWE);
            ConstructThumb(right, Cursors.SizeWE);

            bottomLeft.DragDelta += new DragDeltaEventHandler(HandleBottomLeft);
            bottomRight.DragDelta += new DragDeltaEventHandler(HandleBottomRight);
            topLeft.DragDelta += new DragDeltaEventHandler(HandleTopLeft);
            topRight.DragDelta += new DragDeltaEventHandler(HandleTopRight);
            top.DragDelta += new DragDeltaEventHandler(HandleTop);
            bottom.DragDelta += new DragDeltaEventHandler(HandleBottom);
            left.DragDelta += new DragDeltaEventHandler(HandleLeft);
            right.DragDelta += new DragDeltaEventHandler(HandleRight);
        }

        private void HandleRight(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;

            element.Width = Math.Max(element.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);
        }

        private void HandleLeft(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;

            double OldWidth = element.Width;
            double NewWidth = Math.Max(element.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);
            if (element.Width - e.HorizontalChange > hitThumb.DesiredSize.Width)
            {
                double OldX = element.X;
                element.X = OldX - (OldWidth - NewWidth);
                element.Width = Math.Max(element.Width - e.HorizontalChange, hitThumb.DesiredSize.Width);
            }
        }

        private void HandleBottom(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;

            element.Height = Math.Max(e.VerticalChange + element.Height, hitThumb.DesiredSize.Height);
        }

        private void HandleTop(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;

            double OldHeight = element.Height;
            double NewHeight = Math.Max(e.VerticalChange + element.Height, hitThumb.DesiredSize.Height);
            if (element.Height - e.VerticalChange > hitThumb.DesiredSize.Height)
            {
                double OldY = element.Y;
                element.Y = OldY - (OldHeight - NewHeight);
                element.Height = Math.Max(element.Height - e.VerticalChange, hitThumb.DesiredSize.Height);
            }
        }

        private void HandleBottomRight(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;
            
            element.Width = Math.Max(element.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);
            element.Height = Math.Max(e.VerticalChange + element.Height, hitThumb.DesiredSize.Height);
        }

        private void HandleTopRight(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;
            
            element.Width = Math.Max(element.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);

            double OldHeight = element.Height;
            double NewHeight = Math.Max(e.VerticalChange + element.Height, hitThumb.DesiredSize.Height);
            if (element.Height - e.VerticalChange > hitThumb.DesiredSize.Height)
            {
                double OldY = element.Y;
                element.Y = OldY - (OldHeight - NewHeight);
                element.Height = Math.Max(element.Height - e.VerticalChange, hitThumb.DesiredSize.Height);
            }
        }

        private void HandleTopLeft(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;
            
            double OldWidth = element.Width;
            double NewWidth = Math.Max(element.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);
            if (element.Width - e.HorizontalChange > hitThumb.DesiredSize.Width)
            {
                double OldX = element.X;
                element.X = OldX - (OldWidth - NewWidth);
                element.Width = Math.Max(element.Width - e.HorizontalChange, hitThumb.DesiredSize.Width);
            }

            double OldHeight = element.Height;
            double NewHeight = Math.Max(e.VerticalChange + element.Height, hitThumb.DesiredSize.Height);
            if (element.Height - e.VerticalChange > hitThumb.DesiredSize.Height)
            {
                double OldY = element.Y;
                element.Y = OldY - (OldHeight - NewHeight);
                element.Height = Math.Max(element.Height - e.VerticalChange, hitThumb.DesiredSize.Height);
            }
        }

        private void HandleBottomLeft(object sender, DragDeltaEventArgs e)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            Thumb hitThumb = sender as Thumb;

            double OldWidth = element.Width;
            double NewWidth = Math.Max(element.Width + e.HorizontalChange, hitThumb.DesiredSize.Width);
            if (element.Width - e.HorizontalChange > hitThumb.DesiredSize.Width)
            {
                double OldX = element.X;
                element.X = OldX - (OldWidth - NewWidth);
                element.Width = Math.Max(element.Width - e.HorizontalChange, hitThumb.DesiredSize.Width);
            }

            element.Height = Math.Max(e.VerticalChange + element.Height, hitThumb.DesiredSize.Height);
        }
        
        protected override Size ArrangeOverride(Size finalSize)
        {
            ShapeViewModel element = (this.AdornedElement as UserControl).DataContext as ShapeViewModel;
            double Width = element.Width;
            double Height = element.Height;

            topLeft.Arrange(new Rect(-Width / 2, -Height / 2, Width, Height));
            topRight.Arrange(new Rect(Width - Width / 2, -Height / 2, Width, Height));
            bottomLeft.Arrange(new Rect(-Width / 2, Height - Height / 2, Width, Height));
            bottomRight.Arrange(new Rect(Width - Width / 2, Height - Height / 2, Width, Height));

            top.Arrange(new Rect(0, -Height / 2, Width, Height));
            bottom.Arrange(new Rect(0, Height / 2, Width, Height));
            left.Arrange(new Rect(-Width / 2, 0, Width, Height));
            right.Arrange(new Rect(Width / 2, 0, Width, Height));

            top.Width = Width - (topLeft.Width / 2) - (topRight.Width / 2);
            bottom.Width = Width - (bottomLeft.Width / 2) - (bottomRight.Width / 2);
            left.Height = Height - (topLeft.Height / 2) - (bottomLeft.Height / 2);
            right.Height = Height - (topRight.Height / 2) - (bottomRight.Height / 2);

            return finalSize;
        }
        
        void ConstructThumb(Thumb thumb, Cursor customizedCursor)
        {
            thumb.Cursor = customizedCursor;
            thumb.Height = 10;
            thumb.Width = 10;
            thumb.Opacity = 0.00;

            ResizeShapeCommand command = null;
            thumb.DragStarted += new DragStartedEventHandler((sender, e) => command = new ResizeShapeCommand((this.AdornedElement as UserControl).DataContext as ShapeViewModel));
            thumb.DragCompleted += new DragCompletedEventHandler((sender, e) => command.FinalizeResize());

            visualChildren.Add(thumb);
        }
    }
}