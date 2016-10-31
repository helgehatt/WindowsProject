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

namespace ClassDiagramTool
{
    /// <summary>
    /// Interaction logic for MyShape.xaml
    /// </summary>
    public partial class MyShape : UserControl
    {
        private bool drag = false;
        private Point startPt;
        private int wid;
        private int hei;
        private Point lastLoc;

        public MyShape()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = "I Got Pressed";
        }

        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            drag = true;
            Cursor = Cursors.Hand;
            startPt = e.GetPosition(this.Parent as Canvas);
            wid = (int)this.Width;
            hei = (int)this.Height;
            lastLoc = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            Mouse.Capture((IInputElement)sender);

            e.Handled = true;
        }

        private void Shape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            drag = false;
            Cursor = Cursors.Arrow;
            Mouse.Capture(null);
        }

        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (drag)
                {
                    var newX = (startPt.X + (e.GetPosition(this.Parent as Canvas).X - startPt.X));
                    var newY = (startPt.Y + (e.GetPosition(this.Parent as Canvas).Y - startPt.Y));
                    Point offset = new Point((startPt.X - lastLoc.X), (startPt.Y - lastLoc.Y));
                    var CanvasTop = newY - offset.Y;
                    var CanvasLeft = newX - offset.X;
                    this.SetValue(Canvas.TopProperty, CanvasTop);
                    this.SetValue(Canvas.LeftProperty, CanvasLeft);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
