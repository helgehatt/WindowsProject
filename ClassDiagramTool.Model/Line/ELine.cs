
using System;

namespace ClassDiagramTool.Model
{
    [Serializable]
    public enum ELine
    {
        Aggregation,
        Association,
        Composition,
        Dependency,
        DirectedAssociation,
        Inheritance,
        InterfaceRealization
    }
}