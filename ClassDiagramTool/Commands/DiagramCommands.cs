using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class DiagramCommands
    {
        private MainViewModel MainViewModel;

        private SerializationController Serializer => SerializationController.Instance;

        public ObservableCollection<ShapeViewModel> ShapeViewModels => MainViewModel.ShapeViewModels;
        public ObservableCollection<LineViewModel>  LineViewModels  => MainViewModel.LineViewModels;

        public DiagramCommands(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public void New()
        {
            ShapeViewModels.Clear();
            LineViewModels.Clear();
        }

        public string SaveInitialDirectory = Directory.GetCurrentDirectory();
        public string SavePath = null;

        public void Save() => Save(false);
        public void Save(bool saveAs)
        {
            if (saveAs || SavePath == null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.DefaultExt = "xml";
                dialog.InitialDirectory = SaveInitialDirectory;
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK) return;
                SavePath = dialog.FileName;
            }

            Diagram Diagram = new Diagram()
            {
                Shapes = new List<Shape>(ShapeViewModels.Select(o => o.Shape).ToList()),
                Lines = new List<Line>(LineViewModels.Select(o => o.Line).ToList()),
                ConnectionPoints = new List<ConnectionPoint>()
            };

            foreach (var ShapeViewModel in ShapeViewModels)
                foreach (var ConnectionPointViewModel in ShapeViewModel.ConnectionPointViewModels)
                    Diagram.ConnectionPoints.Add(ConnectionPointViewModel.ConnectionPoint);

            Serializer.AsyncSerializeToFile(Diagram, SavePath);
        }

        public string LoadInitialDirectory = Directory.GetCurrentDirectory();

        public void Load()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK) return;
            string Path = dialog.FileName;

            New();

            Diagram Diagram = Serializer.DeserializeFromFile(Path);

            foreach (var Shape in Diagram.Shapes)
            {
                ShapeViewModel ShapeViewModel = null;
                
                switch (Shape.Type)
                {
                    case EShape.Class       : ShapeViewModel = new ClassViewModel        (Shape) ;  break;
                    case EShape.Enumeration : ShapeViewModel = new EnumerationViewModel  (Shape) ;  break;
                    case EShape.Interface   : ShapeViewModel = new InterfaceViewModel    (Shape) ;  break;
                }

                if (ShapeViewModel == null) { Debug.WriteLine("Load, ShapeViewModel == null"); continue; }

                ShapeViewModels.Add(ShapeViewModel);
            }

            foreach (var ConnectionPoint in Diagram.ConnectionPoints)
            {
                var OnShapeViewModel = GetShapeViewModelByNumber(ConnectionPoint.OnShape);

                if (OnShapeViewModel == null) { Debug.WriteLine("Load, OnShapeViewModel == null"); continue; }

                new ConnectionPointViewModel(ConnectionPoint, OnShapeViewModel);
            }

            foreach (var Line in Diagram.Lines)
            {
                var FromShapeViewModel = GetShapeViewModelByNumber(Line.FromShape);
                var ToShapeViewModel   = GetShapeViewModelByNumber(Line.ToShape  );

                if (FromShapeViewModel == null || ToShapeViewModel == null)
                { Debug.WriteLine("Load, FromShapeViewModel == null || ToShapeViewModel == null"); continue; }

                var From = GetConnectionViewModelByNumber(FromShapeViewModel, Line.FromPoint);
                var To   = GetConnectionViewModelByNumber(ToShapeViewModel  , Line.ToPoint  );

                if (From == null || To == null) { Debug.WriteLine("Load, From == null || To == null"); continue; }

                LineViewModel LineViewModel = null;

                switch (Line.Type)
                {
                    case ELine.Aggregation          : LineViewModel = new AggregationViewModel           (From, To);   break;
                    case ELine.Association          : LineViewModel = new AssociationViewModel           (From, To);   break;
                    case ELine.Composition          : LineViewModel = new CompositionViewModel           (From, To);   break;
                    case ELine.Dependency           : LineViewModel = new DependencyViewModel            (From, To);   break;
                    case ELine.DirectedAssociation  : LineViewModel = new DirectedAssociationViewModel   (From, To);   break;
                    case ELine.Inheritance          : LineViewModel = new InheritanceViewModel           (From, To);   break;
                    case ELine.InterfaceRealization : LineViewModel = new InterfaceRealizationViewModel  (From, To);   break;
                }

                if (LineViewModel == null) { Debug.WriteLine("Load, LineViewModel == null"); continue; }

                LineViewModels.Add(LineViewModel);
            }
        }

        private ShapeViewModel GetShapeViewModelByNumber(int number)
        {
            foreach (var ShapeViewModel in ShapeViewModels)
            {
                if (ShapeViewModel.Number == number) return ShapeViewModel;
            }
            return null;
        }

        private ConnectionPointViewModel GetConnectionViewModelByNumber(ShapeViewModel shapeViewModel, int number)
        {
            foreach (var ConnectionPointViewModel in shapeViewModel.ConnectionPointViewModels)
            {
                if (ConnectionPointViewModel.Number == number) return ConnectionPointViewModel;
            }
            return null;
        }
    }
}
