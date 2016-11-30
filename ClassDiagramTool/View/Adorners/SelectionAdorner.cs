using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ClassDiagramTool.View.Adorners
{
    class SelectionAdorner : Adorner
    {
        public SelectionAdorner(UIElement adornedElement) : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRoundedRectangle(new SolidColorBrush(Colors.Transparent), new Pen(new SolidColorBrush(Colors.Navy), 3.0), new Rect(this.AdornedElement.DesiredSize), 5.0, 5.0);
        }
    }
}
