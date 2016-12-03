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

        private ObservableCollection<ShapeViewModel> ShapeViewModels => MainViewModel.ShapeViewModels;
        private ObservableCollection<LineViewModel>  LineViewModels  => MainViewModel.LineViewModels;

        public DiagramCommands(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public void New()
        {
            DialogResult result = MessageBox.Show("Do you want to save your current project?", "WindowsProject",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
            if (result == DialogResult.Yes)
                Save();
            else if (result == DialogResult.Cancel) return;

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
                Shapes           = new List<Shape>(ShapeViewModels.Select(o => o.Shape).ToList()),
                Lines            = new List<Line>(LineViewModels.Select(o => o.Line).ToList()),
                ConnectionPoints = new List<ConnectionPoint>(ShapeViewModels.SelectMany(o => o.ConnectionPointViewModels.Select(p => p.ConnectionPoint)).ToList())
            };

            Serializer.AsyncSerializeToFile(Diagram, SavePath);
        }

        public string LoadInitialDirectory = Directory.GetCurrentDirectory();

        public void Load()
        {
            DialogResult resultSave = MessageBox.Show("Do you want to save your current project?", "WindowsProject",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
            if (resultSave == DialogResult.Yes)
                Save();
            else if (resultSave == DialogResult.Cancel) return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK) return;
            string Path = dialog.FileName;

            ShapeViewModels.Clear();
            LineViewModels.Clear();

            Diagram Diagram = Serializer.DeserializeFromFile(Path);

            var AddedShapeViewModels = MainViewModel.ReconstructShapes(Diagram.Shapes);

            MainViewModel.ReconstructConnectionPoints(Diagram.ConnectionPoints, AddedShapeViewModels);

            var AddedLineViewModels = MainViewModel.ReconstructLines(Diagram.Lines, AddedShapeViewModels);
            
            foreach (var ShapeViewModel in AddedShapeViewModels)
                ShapeViewModels.Add(ShapeViewModel);

            foreach (var LineViewModel in AddedLineViewModels)
                LineViewModels.Add(LineViewModel);
        }
    }
}
