
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
        public ICommand WindowCloseCommand { get; set; }
        public ICommand WindowMinimizeCommand { get; set; }
        public ICommand WindowMaximizeCommand { get; set; }
        public ICommand WindowMouseMoveCommand { get; set; }
        
        public string Tag { get; set; }
        public UCBarControlViewModel()
        {
            WindowCloseCommand = new RelayCommand<UserControl>(
                (p) => { return p == null?false:true ; }, 
                (p) => {
                    Window win=(Window) GetWindowParent(p); 
                    if (win != null) 
                        win.Close(); 
                });
            WindowMinimizeCommand = new RelayCommand<UserControl>(
                (p) => { return p == null ? false : true; },
                (p) => {
                    Window win = (Window)GetWindowParent(p);
                    if (win != null)
                    {
                        if (win.WindowState!= WindowState.Minimized)
                        {
                            win.WindowState = WindowState.Minimized;
                        }
                        else
                        {
                            win.WindowState = WindowState.Normal;
                        }
                    }                   
                });
            WindowMaximizeCommand = new RelayCommand<UserControl>(
                (p) => { return p == null ? false : true; },
                (p) => {
                    Window win = (Window)GetWindowParent(p);
                    if (win != null)
                    {
                        if (win.WindowState != WindowState.Maximized)
                        {
                            win.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            win.WindowState = WindowState.Normal;
                        }
                    }

                });

            WindowMouseMoveCommand = new RelayCommand<UserControl>(
                (p) => { return p == null ? false : true; },
                (p) => {
                    Window win = (Window)GetWindowParent(p);
                    if (win != null)
                    {
                        win.DragMove();
                    }

                });
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent!=null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
