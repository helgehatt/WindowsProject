using System;
using AdvancedWPFDemo.Model;

namespace AdvancedWPFDemo.ViewModel.Shapes
{
    public class CircleViewModel:ShapeViewModel
    {
        public CircleViewModel():this(new Shape() { Type=EShape.Circle})
        {
            
        }
        public CircleViewModel(Shape shape) : base(shape)
        {
            Width = 200;
            Height = 200;
            if (shape?.Type != EShape.Circle)
                throw new InvalidOperationException($"Shape added is not a {nameof(EShape.Circle)}");
        }
    }
}
