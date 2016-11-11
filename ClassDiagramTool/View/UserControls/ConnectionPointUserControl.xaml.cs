using ClassDiagramTool.View.Adorners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDiagramTool.View.UserControls
{
    /// <summary>
    /// Interaction logic for ConnectionPointUserControl.xaml
    /// </summary>
    public partial class ConnectionPointUserControl : UserControl
    {
        public ConnectionPointUserControl()
        {
            InitializeComponent();

            Loaded += ConnectionPointUserControl_Loaded;
        }

        void ConnectionPointUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            var adorner = new ConnectionPointAdorner(this);
            adornerLayer.Add(adorner);
        }
    }
}
