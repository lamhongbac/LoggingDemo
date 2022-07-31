using MSAMobApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSAMobApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public bool IsLogined { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
        protected async override void OnAppearing()
        {

            base.OnAppearing();
            if (IsLogined)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            IsLogined = true;
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}