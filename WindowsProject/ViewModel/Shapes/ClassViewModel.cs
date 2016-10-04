using System;
using WindowsProject.Model;

namespace WindowsProject.ViewModel.Shapes
{
    public class ClassViewModel: ShapeViewModel
    {
       

        public ClassViewModel():this(new Shape() { Type=EShape.Class}){}
        public ClassViewModel(Shape shape) : base(shape)
        {
            Height = 300;
            Width = 200;
            if (shape?.Type != EShape.Class)
                throw new InvalidOperationException($"Shape added is not a {nameof(shape)}");
        }
    }
}
