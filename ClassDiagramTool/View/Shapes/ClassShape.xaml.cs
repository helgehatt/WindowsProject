using ClassDiagramTool.Commands;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ClassDiagramTool
{
    /// <summary>
    /// Interaction logic for ClassShape.xaml
    /// </summary>
    public partial class ClassShape : UserControl
    {
        public ClassShape()
        {
            InitializeComponent();
        }

        //private void StartMoveShape(object sender, MouseButtonEventArgs e)
        //{
        //    new MoveShape(this, e);
        //}

        

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
