using ClassDiagramTool.View.UserControls;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class ConnectShapesCommand
    {
        ShapeViewModel From, To;

        public ConnectShapesCommand()
        {
            MouseButtonEventHandler handler = new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) => OnMouseButtonDown(e));
        }

        private void OnMouseButtonDown(MouseButtonEventArgs e)
        {
        }
    }
}
