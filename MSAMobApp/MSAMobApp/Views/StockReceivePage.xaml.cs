﻿using MSAMobApp.Data;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            MobStockMasterItem item = await MSADataBase.GetMasterStockItemAsync(scanResultText.Text.Trim());
            if (item != null)
            {
                _viewModel.Unit = item.Unit;
                _viewModel.Name = item.Name;
            }

        }
    }
}