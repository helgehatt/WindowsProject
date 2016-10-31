using ClassDiagramTool.Commands;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ClassDiagramTool.ViewModel
{
    class MainViewModel
    {
        // MouseClick Add Shape Command
        public RelayCommand<MouseButtonEventArgs> AddShapeCommand => new RelayCommand<MouseButtonEventArgs>(ClickAddShape.Execute, ClickAddShape.CanExecute);
        public ClickAddShape ClickAddShape = new ClickAddShape();

        // MouseClick Move Shape Command
        public RelayCommand<MouseButtonEventArgs> StartMoveShapeCommand => new RelayCommand<MouseButtonEventArgs>(StartMoveShape, o => true);
        public RelayCommand<MouseButtonEventArgs> ProgressMoveShapeCommand => new RelayCommand<MouseButtonEventArgs>(ProgressMoveShape, o => drag);
        public RelayCommand<MouseButtonEventArgs> EndMoveShapeCommand => new RelayCommand<MouseButtonEventArgs>(EndMoveShape, o => drag);

        private bool drag = false;
        private Point startPt;
        private int wid;
        private int hei;
        private Point lastLoc;

        private void StartMoveShape(MouseButtonEventArgs e)
        {
            Debug.WriteLine("StartMoveShape");
            var movedShape = e.Source as ClassShape;
            drag = true;
            movedShape.Cursor = Cursors.Hand;
            startPt = e.GetPosition(movedShape.Parent as Canvas);
            wid = (int) movedShape.Width;
            hei = (int) movedShape.Height;
            lastLoc = new Point(Canvas.GetLeft(movedShape), Canvas.GetTop(movedShape));
            Mouse.Capture(movedShape);
            e.Handled = true;
        }

        private void EndMoveShape(MouseButtonEventArgs e)
        {
            Debug.WriteLine("EndMoveShape");
            var movedShape = e.Source as ClassShape;
            drag = false;
            movedShape.Cursor = Cursors.Arrow;
            Mouse.Capture(null);
        }

        private void ProgressMoveShape(MouseEventArgs e)
        {
            Debug.WriteLine("ProgressMoveShape");
            var movedShape = e.Source as ClassShape;
            try {
                if (drag) {
                    Point pos = e.GetPosition(movedShape.Parent as Canvas);
                    var newX = (startPt.X + pos.X - startPt.X);
                    var newY = (startPt.Y + pos.Y - startPt.Y);
                    Point offset = new Point((startPt.X - lastLoc.X), (startPt.Y - lastLoc.Y));
                    var CanvasTop = newY - offset.Y;
                    var CanvasLeft = newX - offset.X;
                    movedShape.SetValue(Canvas.TopProperty, CanvasTop);
                    movedShape.SetValue(Canvas.LeftProperty, CanvasLeft);

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShapeSizeChanged(SizeChangedEventArgs e) {
            ClassShape shape = e.Source as ClassShape;
            BindingExpression titleHeight = shape.titleText.GetBindingExpression(TextBox.HeightProperty);
            BindingExpression titleWidth = shape.titleText.GetBindingExpression(TextBox.WidthProperty);
            BindingExpression mainHeight = shape.mainText.GetBindingExpression(TextBox.HeightProperty);
            BindingExpression mainWidth = shape.mainText.GetBindingExpression(TextBox.WidthProperty);
            titleHeight.UpdateTarget();
            titleWidth.UpdateTarget();
            mainHeight.UpdateTarget();
            mainWidth.UpdateTarget();
        }

        private void EditText(MouseButtonEventArgs e)
        {
            TextBox text = (TextBox) e.Source;
            text.Focusable = true;
            text.IsReadOnly = false;
            text.SelectAll();
            text.Focus();
        }

        private void FinishEditText(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox text = (TextBox) e.Source;
                text.Select(0, 0);
                text.IsReadOnly = true;
                text.Focusable = false;
                text.IsEnabled = false;
                text.IsEnabled = true;
            }
        }
    }

    
}
