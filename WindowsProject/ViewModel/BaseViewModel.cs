﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AdvancedWPFDemo.Annotations;

namespace AdvancedWPFDemo.ViewModel
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged= delegate { };
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
