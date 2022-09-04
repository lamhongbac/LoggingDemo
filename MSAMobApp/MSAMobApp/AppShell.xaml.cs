using MSAMobApp.ViewModels;
using MSAMobApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FontAwesomeNS;
namespace MSAMobApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            //ScanSampleBarCodePage
            Routing.RegisterRoute(nameof(ScanSampleBarCodePage), typeof(ScanSampleBarCodePage));
            //DemoPage
            Routing.RegisterRoute(nameof(NewStockItem), typeof(NewStockItem));
            //
            Routing.RegisterRoute(nameof(OneStockReceivePage), typeof(OneStockReceivePage));
            Routing.RegisterRoute(nameof(BatchStockReceivePage), typeof(BatchStockReceivePage));

            //Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            //StockSampleDetailViewModel
            Routing.RegisterRoute(nameof(StockItemPage), typeof(StockItemPage));
            Routing.RegisterRoute(nameof(StockReceivePage), typeof(StockReceivePage));

            

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("$//ScanBarCodeDemo");
        }
    }
}
