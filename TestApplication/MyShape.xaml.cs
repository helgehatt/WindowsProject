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

namespace TestApplication
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

        private void grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BindingExpression titleHeight = titleText.GetBindingExpression(TextBox.HeightProperty);
            BindingExpression titleWidth = titleText.GetBindingExpression(TextBox.WidthProperty);
            BindingExpression mainHeight = mainText.GetBindingExpression(TextBox.HeightProperty);
            BindingExpression mainWidth = mainText.GetBindingExpression(TextBox.WidthProperty);
            titleHeight.UpdateTarget();
            titleWidth.UpdateTarget();
            mainHeight.UpdateTarget();
            mainWidth.UpdateTarget();
        }

        private void text_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox text = (TextBox) sender;
            text.Focusable = true;
            text.IsReadOnly = false;
            text.SelectAll();
            text.Focus();
        }

        private void text_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter)
            {
                TextBox text = (TextBox)sender;
                text.Select(0, 0);
                text.IsReadOnly = true;
                text.Focusable = false;
                text.IsEnabled = false;
                text.IsEnabled = true;
            }
        }
    }
}
