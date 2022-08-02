using MSAMobApp.Data;
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
    public partial class ScanSampleBarCodePage : ContentPage
    {
        public ScanSampleBarCodePage()
        {
            InitializeComponent();
            BindingContext = new  NewStockSampleViewModel();
        }

       

        private  void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            string resultText = string.Empty;
            string userID = "Demo";

            //MSADataBase databaseService
            Device.BeginInvokeOnMainThread(async () =>
            {
                resultText = result.Text;
                if (!string.IsNullOrEmpty(resultText))
                {


                    this.scanResultText.Text = result.Text;

                   // +                " (type: " + result.BarcodeFormat.ToString() + ")";


                  //  await MSADataBase.AddStockSample(userID, resultText);

                }
            });
        }
    }
}