using MSAMobApp.Data;
using MSAMobApp.Models;
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
    public partial class StockReceivePage : ContentPage
    {
        StockTransViewModel _viewModel;
        public StockReceivePage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new StockTransViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(50);
            scanResultText.Focus();
        }

        

        private async void scanResultText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string barcode = scanResultText.Text.Trim();
            if (!string.IsNullOrEmpty(barcode))
            {
                MobStockMasterItem item = await MSADataBase.GetMasterStockItemAsync(barcode);
                if (item == null)
                {
                    await App.Current.MainPage.DisplayAlert("Not exist  barcode,please declared", "BarCode check", "OK");
                }
                else
                {
                    this.txtName.Text = item.Name;
                    this.txtUnit.Text = item.Unit;
                }
            }
        }
        /// <summary>
        /// kiem tra xem barcode co ton tai trong masterdata, neu co se add vao danh sach barcode
        /// se move vao viewmodel sau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string scanedBarCode = scanResultText.Text.Trim();
            if (_viewModel.ExistBarCode(scanedBarCode))
            {
                return;
            }
            MobStockMasterItem item = await MSADataBase.GetMasterStockItemAsync(scanedBarCode);
            if (item != null)
            {
                _viewModel.Unit = item.Unit;
                _viewModel.Name = item.Name;
                _viewModel.AddDetail();
            }
            
        }
    }
}