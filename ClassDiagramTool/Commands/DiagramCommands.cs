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

        public void Save()
        {
            Diagram Diagram = new Diagram()
            {
                Shapes = new List<Shape>(ShapeViewModels.Select(o => o.Shape).ToList()),
                Lines  = new List<Line> (LineViewModels .Select(o => o.Line ).ToList())
            };

            Serializer.AsyncSerializeToFile(Diagram, Directory.GetCurrentDirectory() + "\\testSave.XML");
        }

        public void Load()
        {
            New();

            Diagram Diagram = Serializer.DeserializeFromFile(Directory.GetCurrentDirectory() + "\\testSave.XML");

            foreach (Shape Shape in Diagram.Shapes)
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

            foreach (Line Line in Diagram.Lines)
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
