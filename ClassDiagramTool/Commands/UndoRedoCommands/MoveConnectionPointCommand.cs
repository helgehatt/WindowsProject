using ClassDiagramTool.Model;
using ClassDiagramTool.Tools;
using ClassDiagramTool.View.ShapeControls;
using ClassDiagramTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ClassDiagramTool.Commands.UndoRedoCommands
{
    class MoveConnectionPointCommand : IUndoRedoCommand
    {
        private ConnectionPointViewModel ConnectionPointViewModel;

        private double OriginalPercentile;
        private double FinalPercentile;

        private Point OriginalPosition;

        public MoveConnectionPointCommand(ConnectionPointViewModel connectionPointViewModel, MouseButtonEventArgs e)
        {
            ConnectionPointViewModel = connectionPointViewModel;
            if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift)) return;
            OriginalPercentile = ConnectionPointViewModel.Percentile;

            var ConnectionPointUserControl = e.Source as UserControl;
            OriginalPosition = e.GetPosition(ConnectionPointUserControl.Parent as ShapeControl);

            MouseEventHandler MouseMove = new MouseEventHandler(
                (object sender, MouseEventArgs e1) => {
                    UpdateMoveConnectionPoint(e1);
                });

            MouseButtonEventHandler MouseUp = null;
            MouseUp = new MouseButtonEventHandler( //This delegate ends the move command and removes event handlers from element.
                (object sender, MouseButtonEventArgs e2) => {
                    FinalizeMoveConnectionPoint(e2);
                    ConnectionPointUserControl.MouseMove -= MouseMove;
                    ConnectionPointUserControl.MouseLeftButtonUp -= MouseUp;
                });

            ConnectionPointUserControl.MouseMove += MouseMove;
            ConnectionPointUserControl.MouseLeftButtonUp += MouseUp;

            e.Handled = true;
        }

        public void Execute()
        {
            ConnectionPointViewModel.Percentile = FinalPercentile;
        }


        public void UnExecute()
        {
            ConnectionPointViewModel.Percentile = OriginalPercentile;
        }

        private void FinalizeMoveConnectionPoint(MouseButtonEventArgs e)
        {
            if (!OriginalPercentile.Equals(FinalPercentile))
                UndoRedoController.Instance.Execute(this);
        }

        private void UpdateMoveConnectionPoint(MouseEventArgs e)
        {
            switch (ConnectionPointViewModel.Orientation)
            {
                case EConnectionPoint.North:
                case EConnectionPoint.South:
                    double HorizontalChange = e.GetPosition((e.Source as UserControl).Parent as ShapeControl).X - OriginalPosition.X;
                    FinalPercentile = Math.Min(Math.Max(OriginalPercentile + HorizontalChange / ConnectionPointViewModel.ShapeViewModel.Width, 0.0), 1.0);
                    break;
                case EConnectionPoint.East:
                case EConnectionPoint.West:
                    double VerticalChange = e.GetPosition((e.Source as UserControl).Parent as ShapeControl).Y - OriginalPosition.Y;
                    FinalPercentile = Math.Min(Math.Max(OriginalPercentile + VerticalChange / ConnectionPointViewModel.ShapeViewModel.Height, 0.0), 1.0);
                    break;
            }
            ConnectionPointViewModel.Percentile = FinalPercentile;
        }
    }
}
