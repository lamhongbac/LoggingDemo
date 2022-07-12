using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFMaterialDesignStudy.Lib;
using WPFMaterialDesignStudy.View;
using WPFMaterialDesignStudy.View.Outlet;

namespace WPFMaterialDesignStudy.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand WindowLoadedCommand { get; set; }
        bool isLogined;

        public bool IsLogin
        {
            get => isLogined; set => SetProperty(ref isLogined, value); //{ get => "FullName: " + memberProfile.FullName; }
        }
        string loginName;
        public string LoginName
        {
            get { return loginName; }
            set
            {
                loginName = value;
                Title = loginName;
            }
        }
        public ICommand OutletSCMCommand { get; set; }

        public MainViewModel() : base()
        {
            isLogined = false;
            LoginName = "No Name";
            WindowLoadedCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) =>
                {
                    if (p == null)
                        return;
                    if (!IsLogin)
                    {
                        p.Hide();

                        LoginView loginView = new LoginView();
                        loginView.ShowDialog();
                        //var loginVM = loginView.DataContext as LoginViewModel;
                        //if (loginVM == null)
                        //    p.Close();
                        //isLogined = loginVM.isLogined;
                        //if (loginVM.isLogined)
                        //{
                        //    LoginName = loginVM.FullName;
                        //    p.Show();
                        //}
                        //else
                        //{
                        //    p.Close();
                        //}
                        p.Show();
                        IsLogin = true;
                    }
                });

            OutletSCMCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) =>
                {
                    p.Hide();

                    SCMOutletMain outletMainView = new SCMOutletMain();
                    outletMainView.ShowDialog();
                    p.Show();
                }
                );

        }

    }
}
