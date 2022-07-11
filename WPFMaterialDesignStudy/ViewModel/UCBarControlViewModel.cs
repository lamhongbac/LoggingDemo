
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMaterialDesignStudy.Lib;

namespace WPFMaterialDesignStudy.ViewModel
{
   public class UCBarControlViewModel: ViewModelBase
    {
        public ICommand ClosedCommand { get; set; }
        public ICommand MouseMoveCommand { get; set; }
        
        public string Tag { get; set; }
        public UCBarControlViewModel()
        {
            ClosedCommand = new RelayCommand<UserControl>((p) => { return p == null?false:true ; }, (p) => 
            {Window win=(Window) GetWindowParent(p); if (win != null) win.Close(); });

        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (p.Parent!=null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
