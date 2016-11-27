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
                LineViewModel LineViewModel = null;

                ShapeViewModel FromShapeViewModel = GetShapeViewModelByNumber(Line.FromNumber);
                ShapeViewModel ToShapeViewModel   = GetShapeViewModelByNumber(Line.ToNumber  );

                if (FromShapeViewModel == null || ToShapeViewModel == null) { Debug.WriteLine("Load, From == null || To == null"); continue; }
                
                switch (Line.Type)
                {
                    case ELine.Aggregation          : LineViewModel = new AggregationViewModel           (FromShapeViewModel, ToShapeViewModel);   break;
                    case ELine.Association          : LineViewModel = new AssociationViewModel           (FromShapeViewModel, ToShapeViewModel);   break;
                    case ELine.Composition          : LineViewModel = new CompositionViewModel           (FromShapeViewModel, ToShapeViewModel);   break;
                    case ELine.Dependency           : LineViewModel = new DependencyViewModel            (FromShapeViewModel, ToShapeViewModel);   break;
                    case ELine.DirectedAssociation  : LineViewModel = new DirectedAssociationViewModel   (FromShapeViewModel, ToShapeViewModel);   break;
                    case ELine.Inheritance          : LineViewModel = new InheritanceViewModel           (FromShapeViewModel, ToShapeViewModel);   break;
                    case ELine.InterfaceRealization : LineViewModel = new InterfaceRealizationViewModel  (FromShapeViewModel, ToShapeViewModel);   break;
                }

                if (LineViewModel == null) { Debug.WriteLine("Load, LineViewModel == null"); continue; }

                LineViewModels.Add(LineViewModel);
            }
        }

        private ShapeViewModel GetShapeViewModelByNumber(int number)
        {
            foreach (ShapeViewModel ShapeViewModel in ShapeViewModels)
            {
                if (ShapeViewModel.Number == number) return ShapeViewModel;
            }
            return null;
        }
    }
}
