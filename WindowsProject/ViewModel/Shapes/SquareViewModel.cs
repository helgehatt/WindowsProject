using System;
using AdvancedWPFDemo.Model;

namespace AdvancedWPFDemo.ViewModel.Shapes
{
    public class SquareViewModel: ShapeViewModel
    {
       

        public SquareViewModel():this(new Shape() { Type=EShape.Square}){}
        public SquareViewModel(Shape shape) : base(shape)
        {
            Height = 300;
            Width = 200;
            if (shape?.Type != EShape.Square)
                throw new InvalidOperationException($"Shape added is not a {nameof(EShape.Circle)}");
        }
    }
}
