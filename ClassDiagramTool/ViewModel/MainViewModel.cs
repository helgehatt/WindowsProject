using ClassDiagramTool.Commands;
using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.View.ShapeControls;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public RelayCommand NewDiagramCommand    => new RelayCommand(DiagramCommands.New );
        public RelayCommand SaveDiagramCommand   => new RelayCommand(DiagramCommands.Save);
        public RelayCommand SaveAsDiagramCommand => new RelayCommand(() => DiagramCommands.Save(true));
        public RelayCommand LoadDiagramCommand   => new RelayCommand(DiagramCommands.Load);

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

        #region Reconstruction
        public ShapeViewModel GetShapeViewModelByNumber(int number, List<ShapeViewModel> ShapeViewModels)
        {
            foreach (var ShapeViewModel in ShapeViewModels)
            {
                if (ShapeViewModel.Number == number) return ShapeViewModel;
            }
            return null;
        }

        public List<ShapeViewModel> ReconstructShapes(List<Shape> Shapes)
        {
            var AddedShapeViewModels = new List<ShapeViewModel>();

            foreach (var Shape in Shapes)
            {
                ShapeViewModel ShapeViewModel = null;
                
                switch (Shape.Type)
                {
                    case EShape.Class       : ShapeViewModel = new ClassViewModel        (Shape) ;  break;
                    case EShape.Enumeration : ShapeViewModel = new EnumerationViewModel  (Shape) ;  break;
                    case EShape.Interface   : ShapeViewModel = new InterfaceViewModel    (Shape) ;  break;
                }

                if (ShapeViewModel == null) { Debug.WriteLine("ReconstructShapes, ShapeViewModel == null"); continue; }

                AddedShapeViewModels.Add(ShapeViewModel);
            }

            return AddedShapeViewModels;
        }

        public void ReconstructConnectionPoints(List<ConnectionPoint> ConnectionPoints, List<ShapeViewModel> ShapeViewModels)
        {
            foreach (var ConnectionPoint in ConnectionPoints)
            {
                var OnShapeViewModel = GetShapeViewModelByNumber(ConnectionPoint.OnShape, ShapeViewModels);

                if (OnShapeViewModel == null) { Debug.WriteLine("ReconstructConnectionPoints, OnShapeViewModel == null"); continue; }

                new ConnectionPointViewModel(ConnectionPoint, OnShapeViewModel);
            }
        }

        public List<LineViewModel> ReconstructLines(List<Line> Lines, List<ShapeViewModel> ShapeViewModels)
        {
            var AddedLineViewModels = new List<LineViewModel>();

            foreach (var Line in Lines)
            {
                var FromShapeViewModel = GetShapeViewModelByNumber(Line.FromShape, ShapeViewModels);
                var ToShapeViewModel   = GetShapeViewModelByNumber(Line.ToShape  , ShapeViewModels);

                if (FromShapeViewModel == null || ToShapeViewModel == null)
                { Debug.WriteLine("ReconstructLines, FromShapeViewModel == null || ToShapeViewModel == null"); continue; }

                var From = FromShapeViewModel.GetConnectionViewModelByNumber(Line.FromPoint);
                var To   = ToShapeViewModel  .GetConnectionViewModelByNumber(Line.ToPoint  );

                if (From == null || To == null) { Debug.WriteLine("ReconstructLines, From == null || To == null"); continue; }

                LineViewModel LineViewModel = null;

                switch (Line.Type)
                {
                    case ELine.Aggregation          : LineViewModel = new AggregationViewModel          (From, To);  break;
                    case ELine.Association          : LineViewModel = new AssociationViewModel          (From, To);  break;
                    case ELine.Composition          : LineViewModel = new CompositionViewModel          (From, To);  break;
                    case ELine.Dependency           : LineViewModel = new DependencyViewModel           (From, To);  break;
                    case ELine.DirectedAssociation  : LineViewModel = new DirectedAssociationViewModel  (From, To);  break;
                    case ELine.Inheritance          : LineViewModel = new InheritanceViewModel          (From, To);  break;
                    case ELine.InterfaceRealization : LineViewModel = new InterfaceRealizationViewModel (From, To);  break;
                }

                if (LineViewModel == null) { Debug.WriteLine("ReconstructLines, LineViewModel == null"); continue; }

                AddedLineViewModels.Add(LineViewModel);
            }

            return AddedLineViewModels;
        }
        #endregion

    }
}
