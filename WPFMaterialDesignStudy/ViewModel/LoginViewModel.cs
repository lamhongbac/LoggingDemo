using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFMaterialDesignStudy.Lib;

namespace WPFMaterialDesignStudy.ViewModel
{
  public  class LoginViewModel
    {
        public bool isLogined { get; private set; }
        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            isLogined = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; },
                (p)=>{ Login(p); });

        }

        public void Login(Window p)
        {
            if (p == null)
            {
                return;
            }
            isLogined = true;
            p.Close();
        }

    }
}
