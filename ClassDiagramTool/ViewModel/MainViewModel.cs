﻿using ClassDiagramTool.Commands;
using ClassDiagramTool.Model;
using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.View.ShapeControls;
using ClassDiagramTool.View.UserControls;
using ClassDiagramTool.ViewModel.Lines;
using ClassDiagramTool.ViewModel.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ClassDiagramTool.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        #region Fields
        private UndoRedoController UndoRedoController => UndoRedoController.Instance;
        private ShapeViewModel From;
        private Serializer Serializer => Serializer.Instance;
        private SelectedObjectsCollection selectedObjects => SelectedObjectsCollection.Instance;

        public static bool IsAddingLine = false; // Temporary

        public EShape SelectedShape { get; set; }
        public ELine  SelectedLine  { get; set; }
        
        public ObservableCollection<ShapeViewModel> Shapes { get; }
        public ObservableCollection<LineViewModel>  Lines  { get; }
        #endregion

        #region Commands
        public ICommand UndoCommand => UndoRedoController.UndoCommand;
        public ICommand RedoCommand => UndoRedoController.RedoCommand;
        
        public RelayCommand<MouseButtonEventArgs> AddShapeCommand => new RelayCommand<MouseButtonEventArgs>(OnAddShapeCommand, e => e.Source is Canvas);
        public RelayCommand<MouseButtonEventArgs> AddLineCommand => new RelayCommand<MouseButtonEventArgs>(OnAddLineCommand, e => e.Source is ShapeControl && IsAddingLine);
        public RelayCommand<MouseButtonEventArgs> SelectShapeCommand => new RelayCommand<MouseButtonEventArgs>((e) => new SelectObjectCommand(e).Execute(), e => e.Source is ShapeControl);
        public RelayCommand<MouseButtonEventArgs> CutShapeCommand => new RelayCommand<MouseButtonEventArgs>(OnCutShapeCommand);
        public RelayCommand<MouseButtonEventArgs> CopyShapeCommand => new RelayCommand<MouseButtonEventArgs>(OnCopyShapeCommand);
        public RelayCommand<MouseButtonEventArgs> PasteShapeCommand => new RelayCommand<MouseButtonEventArgs>(OnPasteShapeCommand);
        public RelayCommand<MouseButtonEventArgs> SaveDiagramCommand => new RelayCommand<MouseButtonEventArgs>(OnSaveDiagramCommand);
        public RelayCommand<MouseButtonEventArgs> LoadDiagramCommand => new RelayCommand<MouseButtonEventArgs>(OnLoadDiagramCommand);

        public RelayCommand IsAddingLineChange => new RelayCommand(() => { IsAddingLine = !IsAddingLine; }); // Temporary
        #endregion

        #region CommandMethods


        private void OnSaveDiagramCommand(MouseButtonEventArgs e)
        {
            Diagram diagram = new Diagram()
            {
                Shapes = new List<Shape>(Shapes.Select(o => o.Shape).ToList()),
                Lines = new List<Line>(Lines.Select(o => o.Line).ToList())
            };
           
            Serializer.AsyncSerializeToFile(diagram, Directory.GetCurrentDirectory()+"\\testSave.XML");
        }

        private void OnLoadDiagramCommand(MouseButtonEventArgs e)
        {
            
            Diagram diagram = Serializer.DeserializeFromFile(Directory.GetCurrentDirectory() + "\\testSave.XML");
            Shapes.Clear();
            foreach(Shape shape in diagram.Shapes)
            {
               
                ShapeViewModel Shape = null;

                switch (shape.Type)
                {
                    case EShape.Class       : Shape = new ClassViewModel        (shape) ;  break;
                    case EShape.Enumeration : Shape = new EnumerationViewModel  (shape) ;  break;
                    case EShape.Interface   : Shape = new InterfaceViewModel    (shape) ;  break;
                }
                Shapes.Add(Shape);
            }
            
        }

        private void OnCutShapeCommand(MouseButtonEventArgs e)
        {
            OnCopyShapeCommand(e);

        }

        private void OnCopyShapeCommand(MouseButtonEventArgs e)
        {
            List<UserControl> selectionList = new List<UserControl>();
            selectionList = selectedObjects.SelectionList;
            List<Shape> shapeList = new List<Shape>();
            foreach(UserControl shape in selectionList)
            {
                shapeList.Add((shape.DataContext as ShapeViewModel).Shape); 
            }
            Clipboard.Clear();
            Clipboard.SetData("ShapeList", shapeList);
        }

        private void OnPasteShapeCommand(MouseButtonEventArgs e)
        {
            if(Clipboard.ContainsData("ShapeList"))
            {
                
                List<Shape> shapeList = Clipboard.GetData("ShapeList") as List<Shape>;
                foreach (Shape shape in shapeList)
                {

                    ShapeViewModel Shape = null;

                    switch (shape.Type)
                    {
                        case EShape.Class: Shape = new ClassViewModel(shape); break;
                        case EShape.Enumeration: Shape = new EnumerationViewModel(shape); break;
                        case EShape.Interface: Shape = new InterfaceViewModel(shape); break;
                    }
                    Shapes.Add(Shape);
                }
            }
        }

        private void OnAddShapeCommand(MouseButtonEventArgs e)
        {
            Debug.WriteLine(e.Source is Canvas);
            Canvas MainCanvas = e.Source as Canvas;

            Point Position = Mouse.GetPosition(MainCanvas);

            ShapeViewModel Shape = null;

            switch (SelectedShape)
            {
                case EShape.Class       : Shape = new ClassViewModel        () { CenterX = Position.X, CenterY = Position.Y };  break;
                case EShape.Enumeration : Shape = new EnumerationViewModel  () { CenterX = Position.X, CenterY = Position.Y };  break;
                case EShape.Interface   : Shape = new InterfaceViewModel    () { CenterX = Position.X, CenterY = Position.Y };  break;
            }

            if (Shape == null) Debug.WriteLine("OnAddShapeCommand, Shape == null, EShape = " + SelectedShape);
            else
            {
                UndoRedoController.AddAndExecute(new AddShapeCommand(Shapes, Shape));
            }
        }

        
        
        private void OnAddLineCommand(MouseButtonEventArgs e)
        {
            UserControl UserControl = e.Source as UserControl;
            if (UserControl == null) return;

            ShapeViewModel Shape = UserControl.DataContext as ShapeViewModel;

                 if (Shape == null) Debug.WriteLine("OnAddLineCommand, DataContext=" + (e.Source as UserControl).DataContext);
            else if (From  == null) From = Shape;
            else if (From  != Shape)
            {
                LineViewModel Line = null;
                switch (SelectedLine)
                {
                    case ELine.Aggregation          : Line = new AggregationViewModel           (From, Shape);   break;
                    case ELine.Association          : Line = new AssociationViewModel           (From, Shape);   break;
                    case ELine.Composition          : Line = new CompositionViewModel           (From, Shape);   break;
                    case ELine.Dependency           : Line = new DependencyViewModel            (From, Shape);   break;
                    case ELine.DirectedAssociation  : Line = new DirectedAssociationViewModel   (From, Shape);   break;
                    case ELine.Inheritance          : Line = new InheritanceViewModel           (From, Shape);   break;
                    case ELine.InterfaceRealization : Line = new InterfaceRealizationViewModel  (From, Shape);   break;
                }
                if (Line == null) Debug.WriteLine("OnAddLineCommand, Line == null, ELine = " + SelectedLine);
                else
                {
                    UndoRedoController.AddAndExecute(new AddLineCommand(Lines, Line));
                    From = null;
                }
            }            
        }
        #endregion

        public MainViewModel() : base()
        {
            Shapes = new ObservableCollection<ShapeViewModel>();
            Lines  = new ObservableCollection<LineViewModel>();
        }
    }


}
