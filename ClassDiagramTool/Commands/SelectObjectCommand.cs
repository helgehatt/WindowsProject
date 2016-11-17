using ClassDiagramTool.ViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class SelectObjectCommand : ICommand
    {
        private MouseButtonEventArgs e;
        private SelectedObjectsCollection SelectedObjectsCollection => SelectedObjectsCollection.Instance;

        public SelectObjectCommand(MouseButtonEventArgs e)
        {
            this.e = e;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute() => Execute(e.Source);
        public void Execute(object parameter)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (SelectedObjectsCollection.IsSelected(parameter as UserControl))
                    SelectedObjectsCollection.Deselect(parameter as UserControl);
                else
                    SelectedObjectsCollection.AddSelect(parameter as UserControl);
            }
            else
                SelectedObjectsCollection.Select(parameter as UserControl);
        }
    }
}
