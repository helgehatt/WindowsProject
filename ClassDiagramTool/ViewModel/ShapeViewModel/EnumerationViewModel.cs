using ClassDiagramTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramTool.ViewModel
{
    class EnumerationViewModel : ShapeViewModel
    {
        public EnumerationViewModel() 
            : this(new Shape() { Type = EShape.Enumeration })
        {
        }

        public EnumerationViewModel(Shape shape)
            : base(shape)
        {
        }
    }
}
