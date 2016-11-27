using ClassDiagramTool.View.Adorners;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ClassDiagramTool.Tools
{
    class SelectedObjectsController
    {
        private static readonly SelectedObjectsController Self = new SelectedObjectsController();
        public static SelectedObjectsController Instance => Self;

        public readonly List<UserControl> SelectionList = new List<UserControl>();

        private SelectedObjectsController() {}

        public void AddSelect(UserControl element)
        {
            //(element.DataContext as ShapeViewModel).Selected = true;
            SelectionList.Add(element);
            AdornerLayer.GetAdornerLayer(element).Add(new ResizeAdorner(element));
            AdornerLayer.GetAdornerLayer(element).Add(new SelectionAdorner(element));
            AdornerLayer.GetAdornerLayer(element).Add(new ConnectionPointAdorner(element));
        }

        public void Select(UserControl element)
        {
            SelectionList.ForEach((current) => {
                //(current.DataContext as ShapeViewModel).Selected = false;
                RemoveAdorner(current);
            });
            SelectionList.Clear();
            AddSelect(element);
        }

        public void Deselect(UserControl element)
        {
            //(element.DataContext as ShapeViewModel).Selected = false;
            SelectionList.Remove(element);
            RemoveAdorner(element);
        }

        void RemoveAdorner(UIElement element)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
            if (layer != null)
            {
                Adorner[] adorners = layer.GetAdorners(element);
                if (adorners != null)
                {
                    foreach (Adorner adorner in AdornerLayer.GetAdornerLayer(element).GetAdorners(element))
                    {
                        if (adorner is SelectionAdorner || adorner is ResizeAdorner || adorner is ConnectionPointAdorner)
                            AdornerLayer.GetAdornerLayer(element).Remove(adorner);
                    }
                }
            }
        }

        public bool IsSelected(UserControl element) => SelectionList.Contains(element);
        public UserControl Get(int n) => SelectionList[n];
        public int Count => SelectionList.Count;
    }
}
