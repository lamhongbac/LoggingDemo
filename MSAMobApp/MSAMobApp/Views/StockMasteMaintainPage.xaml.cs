﻿using MSAMobApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/// <summary>
/// danh sach cac sample barcode da scaned
/// </summary>
namespace MSAMobApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockMasteMaintainPage : ContentPage
    {
        public StockMasteMaintainPage()
        {
            InitializeComponent();
            BindingContext = new StockSamplesViewModel();
        }
    }
}