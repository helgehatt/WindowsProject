using ClassDiagramTool.View.UserControls;
using ClassDiagramTool.ViewModel.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class ConnectShapesCommand
    {
        ShapeViewModel From, To;

        public ConnectShapesCommand(MouseButtonEventArgs e)
        {
            //MouseButtonEventHandler handler = new MouseButtonEventHandler((object sender, MouseButtonEventArgs e1) => { });
            Debug.WriteLine(e.OriginalSource.GetType());
            Debug.WriteLine("Is a UserControl: " + (e.OriginalSource is UserControl));
            Debug.WriteLine(e.Source.GetType());
            Debug.WriteLine("Is a UserControl: " + (e.Source is UserControl));
            if (e.Source is UserControl)
            {
                Debug.WriteLine((e.Source as UserControl).DataContext);
                ((ShapeViewModel)(e.Source as UserControl).DataContext).X = 0;
                ((ShapeViewModel)(e.Source as UserControl).DataContext).Y = 0;
                Debug.WriteLine(((ShapeViewModel)(e.Source as UserControl).DataContext).MoveShapeCommand.GetType());
                //I has found the viewmodel!
            }
        }

        private void OnMouseButtonDown(MouseButtonEventArgs e)
        {
        }
    }
}
