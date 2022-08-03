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
    public partial class DemoPage : ContentPage
    {
        NewStockSampleViewModel _viewModel;
        public DemoPage()
        {
            InitializeComponent();           
            BindingContext = _viewModel = new NewStockSampleViewModel();
            this.scanResultText.Focus();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.scanResultText.Focus();
        }
    }
}