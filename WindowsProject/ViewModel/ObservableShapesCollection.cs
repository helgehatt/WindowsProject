using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WindowsProject.ViewModel.Shapes;

namespace WindowsProject.ViewModel
{
    public delegate void GlobalStatusChanged(ObservableShapesCollection sender, GlobalStatusChangedEventArgs e);

    public class GlobalStatusChangedEventArgs:EventArgs
    {
        public EGlobalShapeStatus Status { get; }
        public GlobalStatusChangedEventArgs(EGlobalShapeStatus status)
        {
            Status = status;
        }
    }

    public interface IGlobalShapeStatusChanged
    {
        void GlobalStatus(ObservableShapesCollection sender, GlobalStatusChangedEventArgs e );
    }

   
    public class ObservableShapesCollection:ObservableCollection<ShapeViewModel>
    {
        private EGlobalShapeStatus _globalStatus;

        public new void  Add(ShapeViewModel shape)
        {
            if (!(shape is IGlobalShapeStatusChanged))
                throw new ArgumentException($"implementation of {nameof(IGlobalShapeStatusChanged)} is Required ");
            GlobalStatusChangedHandler += (shape as IGlobalShapeStatusChanged).GlobalStatus;
            base.Add(shape);
        }

        public new bool Remove(ShapeViewModel shape)
        {
            if (!(shape is IGlobalShapeStatusChanged))
                throw new ArgumentException($"implementation of {nameof(IGlobalShapeStatusChanged)} is Required ");
            GlobalStatusChangedHandler -= (shape as IGlobalShapeStatusChanged).GlobalStatus;
            
            return base.Remove(shape);
        }

        public new void SetItem(int index, ShapeViewModel shape)
        {
            GlobalStatusChangedHandler -= this[index].GlobalStatus;
            base.SetItem(index, shape);
            GlobalStatusChangedHandler += shape.GlobalStatus;
        }

        public new void InsertItem(int index, ShapeViewModel shape)
        {
            GlobalStatusChangedHandler += shape.GlobalStatus;
            base.InsertItem(index, shape);
        }

        private event GlobalStatusChanged GlobalStatusChangedHandler = delegate { };
        public EGlobalShapeStatus GlobalStatus
        {
            get { return _globalStatus; }
            set
            {
                _globalStatus = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(GlobalStatus)));
                    OnGlobalStatusChanged(value);
            }
        }


        protected virtual void OnGlobalStatusChanged(EGlobalShapeStatus status)
        {
            GlobalStatusChangedHandler(this, new GlobalStatusChangedEventArgs(status) );
        }
    }
}
