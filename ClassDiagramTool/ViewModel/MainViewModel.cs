﻿using ClassDiagramTool.Commands;
using ClassDiagramTool.UndoRedo;
using ClassDiagramTool.ViewModel.Lines;
using ClassDiagramTool.ViewModel.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public ObservableCollection<LineViewModel> Lines { get; }
        public ObservableCollection<ShapeViewModel> Shapes { get; }

        private Random rand = new Random();
        #endregion

        #region Commands
        public ICommand UndoCommand => UndoRedoController.UndoCommand;
        public ICommand RedoCommand => UndoRedoController.RedoCommand;
        
        public RelayCommand<MouseButtonEventArgs> AddShapeCommand   => new RelayCommand<MouseButtonEventArgs>(OnAddShapeCommand);
        public RelayCommand<MouseButtonEventArgs> AddLineCommand    => new RelayCommand<MouseButtonEventArgs>(OnAddLineCommand);
        public RelayCommand<MouseButtonEventArgs> MoveShapeCommand  => new RelayCommand<MouseButtonEventArgs>((e) => new MoveShape(e));
        #endregion

        #region CommandMethods
        private void OnAddShapeCommand(object parameter)
        {
            if (parameter == null)
                throw new InvalidOperationException();

            MouseButtonEventArgs e = parameter as MouseButtonEventArgs;
            Canvas mainCanvas = e.Source as Canvas;

            Point position = Mouse.GetPosition(mainCanvas);

            ShapeViewModel Shape = new SquareViewModel() { CenterX = position.X, CenterY = position.Y, Title = "Title", Text = new List<string> { "text1", "text2", "text3" } };

            UndoRedoController.AddAndExecute(new AddShapeCommand(Shapes, Shape));
        }

        private void OnAddLineCommand(object parameter)
        {
            if (parameter == null)
                throw new InvalidOperationException();

            MouseButtonEventArgs e = parameter as MouseButtonEventArgs;
            Canvas mainCanvas = e.Source as Canvas;

            Point position = Mouse.GetPosition(mainCanvas);

            LineViewModel Line = new SolidLineViewModel() { };

            UndoRedoController.AddAndExecute(new AddLineCommand(Lines, Line));
        }
        #endregion

        public MainViewModel() : base()
        {
            Shapes = new ObservableCollection<ShapeViewModel>();
            Lines = new ObservableCollection<LineViewModel>();
        }
    }


}
