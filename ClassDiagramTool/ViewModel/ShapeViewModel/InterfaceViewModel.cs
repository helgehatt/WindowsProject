using ClassDiagramTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.ViewModel
{
    class InterfaceViewModel : ShapeViewModel
    {
        public InterfaceViewModel() 
            : this(new Shape() { Type = EShape.Interface })
        {
        }

        public InterfaceViewModel(Shape shape)
            : base(shape)
        {
        }
    }
}
