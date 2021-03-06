﻿using ClassDiagramTool.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDiagramTool.Commands
{
    class EditTextCommand : IUndoRedoCommand
    {
        private TextBox EditedTextBox;
        private string OriginalText;
        private string NewText;

        public EditTextCommand(MouseButtonEventArgs e)
        {
            EditedTextBox = e.Source as TextBox;
            OriginalText = EditedTextBox.Text;

            KeyEventHandler enter = null;
            RoutedEventHandler lostfocus = null;
            enter = new KeyEventHandler(
                (object sender, KeyEventArgs e1) => {
                    if (e1.Key == Key.Enter)
                    {
                        lockText();
                        EditedTextBox.KeyDown -= enter;
                        EditedTextBox.LostFocus -= lostfocus;
                    }
                });
            lostfocus = new RoutedEventHandler(
                (object sender, RoutedEventArgs e2) => {
                    lockText();
                    EditedTextBox.KeyDown -= enter;
                    EditedTextBox.LostFocus -= lostfocus;
                });

            EditedTextBox.KeyDown += enter;
            EditedTextBox.LostFocus += lostfocus;

            EditedTextBox.Focusable = true;
            EditedTextBox.IsReadOnly = false;
            //EditedTextBox.SelectAll();
            EditedTextBox.Focus();
            e.Handled = true;
        }

        public void Execute()
        {
            EditedTextBox.Text = NewText;
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
            if (!OriginalText.Equals(NewText))
                UndoRedoController.Instance.Execute(this);
        }
    }
}
