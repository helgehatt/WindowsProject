using System;
using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel.Shapes
{
    public class SquareViewModel : ShapeViewModel
    {       
        public SquareViewModel() 
            : this(new Shape() { Type=EShape.Square })
        {
        }

        public SquareViewModel(Shape shape)
            : base(shape)
        {
        }
    }
}
