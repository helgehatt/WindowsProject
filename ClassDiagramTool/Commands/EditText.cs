using ClassDiagramTool.UndoRedo;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class EditText : IUndoRedoCommand
    {
        private TextBox EditedTextBox;
        private string OriginalText;
        private string NewText;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
        public void Execute(object parameter)
        {
            EditedTextBox = (TextBox)parameter;
            OriginalText = EditedTextBox.Text;

            KeyEventHandler enter = null;
            RoutedEventHandler lostfocus = null;
            enter = new KeyEventHandler(
                (object sender, KeyEventArgs e) => {
                    if (e.Key == Key.Enter) {
                        lockText();
                        EditedTextBox.KeyDown -= enter;
                        EditedTextBox.LostFocus -= lostfocus;
                    }
                });
            lostfocus = new RoutedEventHandler(
                (object sender, RoutedEventArgs e) => {
                    lockText();
                    EditedTextBox.KeyDown -= enter;
                    EditedTextBox.LostFocus -= lostfocus;
                });

            EditedTextBox.KeyDown += enter;
            EditedTextBox.LostFocus += lostfocus;

            EditedTextBox.Focusable = true;
            EditedTextBox.IsReadOnly = false;
            EditedTextBox.SelectAll();
            EditedTextBox.Focus();

        }

        public void UnExecute()
        {
            EditedTextBox.Text = OriginalText;
        }

        private void lockText()
        {
            NewText = EditedTextBox.Text;
            EditedTextBox.Select(0, 0);
            EditedTextBox.IsReadOnly = true;
            EditedTextBox.Focusable = false;
            EditedTextBox.IsEnabled = false;
            EditedTextBox.IsEnabled = true;
        }
    }
}
