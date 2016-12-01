using ClassDiagramTool.Commands;
using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.View.ShapeControls;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ClassDiagramTool.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        #region Fields
        private SelectedObjectsController SelectedObjectsController => SelectedObjectsController.Instance;
        private UndoRedoController        UndoRedoController        => UndoRedoController.Instance;

        private ClipboardCommands ClipboardCommands;
        private DiagramCommands   DiagramCommands;
        private ObjectCommands    ObjectCommands;

        private string _statusText = "Ready";
        public string StatusText
        {
            get { return _statusText; }
            set {
                _statusText = value;
                OnPropertyChanged();
            }
        }

        private bool _blurredCanvas = false;
        public bool BlurredCanvas
        {
            get { return _blurredCanvas; }
            set {
                _blurredCanvas = value;
                OnPropertyChanged();
            }
        }

        public EShape SelectedShape { get; set; }
        public ELine  SelectedLine  { get; set; }
        
        public ObservableCollection<ShapeViewModel> ShapeViewModels { get; } = new ObservableCollection<ShapeViewModel>();
        public ObservableCollection<LineViewModel>  LineViewModels  { get; } = new ObservableCollection<LineViewModel>();
        #endregion

        #region Commands
        public RelayCommand UndoCommand => UndoRedoController.UndoCommand;
        public RelayCommand RedoCommand => UndoRedoController.RedoCommand;
        
        /* Object Commands */
        public RelayCommand<MouseButtonEventArgs> AddShapeCommand           => new RelayCommand<MouseButtonEventArgs>(ObjectCommands.AddShapes         , e => e.Source is Canvas);
        public RelayCommand<MouseButtonEventArgs> AddLineCommand            => new RelayCommand<MouseButtonEventArgs>(ObjectCommands.AddLine           , e => e.Source is UserControl);
        public RelayCommand<MouseButtonEventArgs> AddConnectionPointCommand => new RelayCommand<MouseButtonEventArgs>(ObjectCommands.AddConnectionPoint, e => e.Source is ShapeControl);
        public RelayCommand<MouseButtonEventArgs> SelectShapeCommand        => new RelayCommand<MouseButtonEventArgs>(ObjectCommands.SelectShape       , e => e.Source is ShapeControl);
        public RelayCommand                       DeleteShapeCommand        => new RelayCommand                      (ObjectCommands.DeleteShapes      ,( )=> SelectedObjectsController.Count > 0);

        public RelayCommand StartAddingLineCommand => new RelayCommand(ObjectCommands.StartAddingLine);
        public RelayCommand StopAddingLineCommand  => new RelayCommand(ObjectCommands.StopAddingLine );

        /* Diagram Commands */
        public RelayCommand NewDiagramCommand  => new RelayCommand(DiagramCommands.New );
        public RelayCommand SaveDiagramCommand => new RelayCommand(DiagramCommands.Save);
        public RelayCommand SaveAsDiagramCommand => new RelayCommand(() => {DiagramCommands.Save(true);});
        public RelayCommand LoadDiagramCommand => new RelayCommand(DiagramCommands.Load);

        /* Clipboard Commands */
        public RelayCommand CutShapeCommand    => new RelayCommand(ClipboardCommands.Cut  , () => SelectedObjectsController.Count > 0);
        public RelayCommand CopyShapeCommand   => new RelayCommand(ClipboardCommands.Copy , () => SelectedObjectsController.Count > 0);
        public RelayCommand PasteShapeCommand  => new RelayCommand(ClipboardCommands.Paste, () => Clipboard.ContainsData("Shapes")   );
        #endregion

        public MainViewModel() : base()
        {
            ClipboardCommands = new ClipboardCommands(this);
            DiagramCommands   = new DiagramCommands(this);
            ObjectCommands    = new ObjectCommands(this);
        }
    }


}
