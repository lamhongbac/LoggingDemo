using MSAMobApp.Data;
using MSAMobApp.DataBase;
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
            justScanedBarCode.Focus();
        }

        

        private async void scanResultText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string barcode = justScanedBarCode.Text.Trim();
            if (!string.IsNullOrEmpty(barcode))
            {
                MobStockMasterItem item = await MSADataBase.GetMasterStockItemAsync(barcode);
                if (item == null)
                {
                    await App.Current.MainPage.DisplayAlert("Not exist  barcode,please declared", "BarCode check", "OK");
                }
                else
                {
                    //this.txtName.Text = item.Name;
                    //this.txtUnit.Text = item.Unit;
                    _viewModel.AddDetail();
                }
            }
        }

        private void txtQuantity_Focused(object sender, FocusEventArgs e)
        {
            //txtQuantity.SelectionLength;
            txtQuantity.CursorPosition = 0;
            txtQuantity.SelectionLength = txtQuantity.Text.Length;
        }

        /// <summary>
        /// kiem tra xem barcode co ton tai trong masterdata, neu co se add vao danh sach barcode
        /// se move vao viewmodel sau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void Button_Clicked(object sender, EventArgs e)
        {
            _viewModel.Quantity = 1;
            justScanedBarCode.Focus();
        }

        private void justScanedBarCode_Focused(object sender, FocusEventArgs e)
        {
            justScanedBarCode.CursorPosition = 0;
            if (!string.IsNullOrEmpty(justScanedBarCode.Text))
                justScanedBarCode.SelectionLength = justScanedBarCode.Text.Length;
        }
    }
}