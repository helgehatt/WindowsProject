using ClassDiagramTool.View.Adorners;
using ClassDiagramTool.ViewModel.Adorners;
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
            SelectionList.Add(element);
            AdornerLayer.GetAdornerLayer(element).Add(new ResizeAdorner(element));
            AdornerLayer.GetAdornerLayer(element).Add(new SelectionAdorner(element));
        }

        public void Select(UserControl element)
        {
            SelectionList.ForEach((current) => {
                RemoveAdorner(current);
            });
            SelectionList.Clear();
            AddSelect(element);
        }

        public void Deselect(UserControl element)
        {
            SelectionList.Remove(element);
            RemoveAdorner(element);
        }

        public void DeselectAll()
        {
            foreach (var element in SelectionList)
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
                        if (adorner is SelectionAdorner || adorner is ResizeAdorner)
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
