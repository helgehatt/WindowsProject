﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WindowsProject.ViewModel;
using WindowsProject.ViewModel.Lines;
using WindowsProject.ViewModel.Shapes;

namespace WindowsProject.UndoRedo.Commands
{
    public class RemoveShapesCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> shapes;

        private ObservableCollection<LineViewModel> lines;
        
        private List<ShapeViewModel> shapesToRemove;
        
        private List<LineViewModel> linesToRemove;
        public RemoveShapesCommand(ObservableCollection<ShapeViewModel> _shapes, ObservableCollection<LineViewModel> _lines, List<ShapeViewModel> _shapesToRemove)
        {
            shapes = _shapes;
            lines = _lines;
            shapesToRemove = _shapesToRemove;
            linesToRemove = _lines.Where(x => _shapesToRemove.Any(y => y.Number == x.From.Number || y.Number == x.To.Number)).ToList();
        }
        
        public void Execute()
        {
            linesToRemove.ForEach(x => lines.Remove(x));
            shapesToRemove.ForEach(x => shapes.Remove(x));
        }
        
        public void UnExecute()
        {
            shapesToRemove.ForEach(x => shapes.Add(x));
            linesToRemove.ForEach(x => lines.Add(x));
        }
    }
}
