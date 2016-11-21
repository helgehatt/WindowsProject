using ClassDiagramTool.View.Adorners;
using ClassDiagramTool.ViewModel.Shapes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
            (element.DataContext as ShapeViewModel).Selected = true;
            SelectionList.Add(element);
            AdornerLayer.GetAdornerLayer(element).Add(new ResizeAdorner(element));
        }

        public void Select(UserControl element)
        {
            SelectionList.ForEach((current) => { (current.DataContext as ShapeViewModel).Selected = false;
                foreach (Adorner adorner in AdornerLayer.GetAdornerLayer(current).GetAdorners(current))
                {
                    AdornerLayer.GetAdornerLayer(current).Remove(adorner);
                }
            });
            SelectionList.Clear();
            AddSelect(element);
        }

        public void Deselect(UserControl element)
        {
            (element.DataContext as ShapeViewModel).Selected = false;
            SelectionList.Remove(element);
            foreach (Adorner adorner in AdornerLayer.GetAdornerLayer(element).GetAdorners(element))
            {
                AdornerLayer.GetAdornerLayer(element).Remove(adorner);
            }
        }

        public bool IsSelected(UserControl element) => SelectionList.Contains(element);
        public UserControl Get(int n) => SelectionList[n];
        public int Count => SelectionList.Count;
    }
}
