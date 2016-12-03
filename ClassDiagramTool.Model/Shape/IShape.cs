using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassDiagramTool.Model
{
 
    public interface IShape
    {
        int Number { get; }

        double X { get; }
        double Y { get; }

        double Width { get; }
        double Height { get; }

        EShape Type { get; }

        string Title { get; }
        ObservableCollection<TextItem> Text { get; }
    }
}