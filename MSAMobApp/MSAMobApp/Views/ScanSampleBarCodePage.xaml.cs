using MSAMobApp.Data;
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
    public partial class ScanSampleBarCodePage : ContentPage
    {
        public ScanSampleBarCodePage()
        {
            InitializeComponent();
        }
        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
           // MSADataBase databaseService
            Device.BeginInvokeOnMainThread(() =>
            {
                //this.scanResultText.Text = result.Text +
                //" (type: " + result.BarcodeFormat.ToString() + ")";
                //MSADataBase.AddStockSample()
            });
        }
    }
}