using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClassDiagramTool.ViewModel
{
    class SelectedObjectsCollection
    {
        private static readonly SelectedObjectsCollection Self = new SelectedObjectsCollection();
        public static SelectedObjectsCollection Instance => Self;

        private readonly List<UserControl> SelectionList = new List<UserControl>();

        private SelectedObjectsCollection():base() {}

        public void AddSelect(UserControl element)
        {
            SelectedView(element);
            SelectionList.Add(element);
        }

        public void Select(UserControl element)
        {
            SelectionList.ForEach(DeSelectView);
            SelectionList.Clear();
            AddSelect(element);
        }

        public void Deselect(UserControl element)
        {
            DeSelectView(element);
            SelectionList.Remove(element);
        }

        public bool IsSelected(UserControl element) => SelectionList.Contains(element);
        public UserControl Get(int n) => SelectionList[n];
        public int Count => SelectionList.Count;

        private Brush SelectionColor = Brushes.Blue;
        private void SelectedView(UserControl element)
        {
            element.BorderBrush = SelectionColor;
            element.BorderThickness = new Thickness(3);
        }

        private void DeSelectView(UserControl element)
        {
            element.BorderBrush = Brushes.Black;
            element.BorderThickness = new Thickness(0);
        }
    }
}
