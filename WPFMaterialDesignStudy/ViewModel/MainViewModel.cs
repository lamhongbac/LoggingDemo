using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFMaterialDesignStudy.Lib;
using WPFMaterialDesignStudy.View;

namespace WPFMaterialDesignStudy.ViewModel
{
   public class MainViewModel:ViewModelBase
    {
        public ICommand WindowLoadedCommand { get; set; }
        bool isLogined = false;
        public MainViewModel() : base()
        {

            WindowLoadedCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) =>
                {
                    //isLogined = true;
                    if (p == null)
                    {
                        return;
                    }

                    //p.Hide();



                    //isLogined = true;
                    LoginView loginView = new LoginView();
                    loginView.ShowDialog();
                    //var loginVM = loginView.DataContext as LoginViewModel;
                    //if (loginVM == null)
                    //    return;

                    //if (loginVM.isLogined)
                    //{
                    //    p.Show();
                    //}
                    //else
                    //{
                    //    p.Close();
                    //}


                });

        }
    }
}
