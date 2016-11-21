using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ClassDiagramTool.View.Adorners
{
    class ConnectionPointAdorner : Adorner
    {
        private ShapeViewModel Shape;

        public ConnectionPointAdorner(UIElement adornedElement) 
            : base(adornedElement)
        {
            UserControl UserControl = adornedElement as UserControl;

            Shape = UserControl.DataContext as ShapeViewModel;
        }

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);
        //
        //    SolidColorBrush brush = new SolidColorBrush(Colors.Black);
        //
        //    foreach (ConnectionPoint p in Shape.P)
        //    {
        //        drawingContext.DrawRectangle(brush, null, new Rect(p.X-4, p.Y-4, 6, 6));
        //    }
        //}

    }
}
