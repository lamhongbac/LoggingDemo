using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFMaterialDesignStudy.Lib;
using WPFMaterialDesignStudy.View;

namespace WPFMaterialDesignStudy.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand WindowLoadedCommand { get; set; }
        bool isLogined;
        public bool IsLoginName { get => isLogined; set => isLogined = value; }
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
        public MainViewModel() : base()
        {
            isLogined = false;
            LoginName = "No Name";
            WindowLoadedCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) =>

                {
                    //isLogined = true;
                    if (p == null)
                    {
                        return;
                    }

                    p.Hide();

                    //isLogined = true;
                    LoginView loginView = new LoginView();
                    loginView.ShowDialog();
                    var loginVM = loginView.DataContext as LoginViewModel;

                    if (loginVM == null)
                        p.Close();

                    isLogined = loginVM.isLogined;


                    if (loginVM.isLogined)
                    {
                        LoginName = loginVM.FullName;
                        p.Show();
                    }
                    else
                    {
                        p.Close();
                    }


                });

        }

        
    }
}
