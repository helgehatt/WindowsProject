using ClassDiagramTool.Commands;
using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.View.ShapeControls;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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

        public static bool IsAddingLine { get; set; }

        public EShape SelectedShape { get; set; }
        public ELine  SelectedLine  { get; set; }
        
        public ObservableCollection<ShapeViewModel> ShapeViewModels { get; }
        public ObservableCollection<LineViewModel>  LineViewModels  { get; }
        #endregion

        #region Commands
        public RelayCommand UndoCommand => UndoRedoController.UndoCommand;
        public RelayCommand RedoCommand => UndoRedoController.RedoCommand;
        
        /* Object Commands */
        public RelayCommand<MouseButtonEventArgs> AddShapeCommand     => new RelayCommand<MouseButtonEventArgs>(ObjectCommands.AddShapes   , e => e.Source is Canvas);
        //public RelayCommand<MouseButtonEventArgs> AddLineCommand      => new RelayCommand<MouseButtonEventArgs>(ObjectCommands.AddLine     , e => e.Source is ShapeControl && IsAddingLine);
        public RelayCommand<MouseButtonEventArgs> SelectShapeCommand  => new RelayCommand<MouseButtonEventArgs>(ObjectCommands.SelectShape , e => e.Source is ShapeControl);
        public RelayCommand                       DeleteShapeCommand  => new RelayCommand                      (ObjectCommands.DeleteShapes,( )=> SelectedObjectsController.Count > 0);
        public RelayCommand                       IsAddingLineCommand => new RelayCommand                      (ObjectCommands.AddingLine );

        /* Diagram Commands */
        public RelayCommand NewDiagramCommand  => new RelayCommand(DiagramCommands.New );
        public RelayCommand SaveDiagramCommand => new RelayCommand(DiagramCommands.Save);
        public RelayCommand LoadDiagramCommand => new RelayCommand(DiagramCommands.Load);

        /* Clipboard Commands */
        public RelayCommand CutShapeCommand    => new RelayCommand(ClipboardCommands.Cut  , () => SelectedObjectsController.Count > 0);
        public RelayCommand CopyShapeCommand   => new RelayCommand(ClipboardCommands.Copy , () => SelectedObjectsController.Count > 0);
        public RelayCommand PasteShapeCommand  => new RelayCommand(ClipboardCommands.Paste, () => Clipboard.ContainsData("Shapes"));
        #endregion

        public MainViewModel() : base()
        {
            ShapeViewModels = new ObservableCollection<ShapeViewModel>();
            LineViewModels  = new ObservableCollection<LineViewModel>();

            ClipboardCommands = new ClipboardCommands(this);
            DiagramCommands   = new DiagramCommands(this);
            ObjectCommands    = new ObjectCommands(this);
        }
    }


}
