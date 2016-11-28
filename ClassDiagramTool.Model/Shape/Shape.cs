using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassDiagramTool.Model
{
    [Serializable]
    public class Shape : IShape
    {
        private static int number;

        public int Number { get; set; } = number++;

        public double X { get; set; }
        public double Y { get; set; }

        public double Width { get; set; } = 250;
        public double Height { get; set; } = 100;

        public EShape Type { get; set; }

        public string Title { get; set; } = "Title";
        public List<TextItem> Text { get; set; } = new List<TextItem>()
        {
            new TextItem() { Value = "text1" },
            new TextItem() { Value = "text2" }
        };
    }

    [Serializable]
    public class TextItem
    {
        public string Value { get; set; }
    }
}
