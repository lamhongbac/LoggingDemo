using MSAMobApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/// <summary>
/// Form Nhap new master code 
/// Usage: khi co sp moi chua co
/// 
/// </summary>
namespace MSAMobApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemoPage : ContentPage
    {
        NewStockItemViewModel _viewModel;
        public DemoPage()
        {
            InitializeComponent();           
            BindingContext = _viewModel = new NewStockItemViewModel();
            this.scanResultText.Focus();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(20);
            this.scanResultText.Focus();
        }
    }
}