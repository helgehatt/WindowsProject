using System;
using ClassDiagramTool.Model;

namespace ClassDiagramTool.ViewModel
{
    public class ClassViewModel : ShapeViewModel
    {       
        public ClassViewModel() 
            : this(new Shape() { Type = EShape.Class })
        {
        }

        public ClassViewModel(Shape shape)
            : base(shape)
        {
        }
    }
}
