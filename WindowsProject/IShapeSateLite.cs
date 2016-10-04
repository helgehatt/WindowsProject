using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsProject
{
    public delegate void ShapeSateLiteEventHandler(object sender, AllShapesEventArgs e);
    public class AllShapesEventArgs : EventArgs
    {
        private EGlobalShapeStatus _status;

        public AllShapesEventArgs(EGlobalShapeStatus status)
        {
            _status = status;
        }
    }

    public enum EGlobalShapeStatus
    {
        Default,
        EnableAll,
        DisableAll,
        SelectAll,
        DeSelectAll,
        ConnectShapes
    }

    //public class ShapeSateLite:Singleton<ShapeSateLite>
    //{
    //    private ShapeSateLiteEventHandler _shapesEventHandler= delegate {};
    //
    //    public event ShapeSateLiteEventHandler ShapesEventHandler
    //    {
    //        add { _shapesEventHandler += value; }
    //        remove {  _shapesEventHandler -= value; }
    //    }
    //
    //    public void Invoke(object sender, EGlobalShapeStatus status)
    //        => _shapesEventHandler(sender, new AllShapesEventArgs(status));
    //}
}
