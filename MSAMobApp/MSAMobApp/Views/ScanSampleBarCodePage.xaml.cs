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
    /// <summary>
    /// Usage: tu view danh sach barcode (sample)
    /// bam new barcode sample=> open this form
    /// quet barcode, type name and unit and bam SAVE button
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanSampleBarCodePage : ContentPage
    {
        NewStockSampleViewModel _viewModel;
        public ScanSampleBarCodePage()
        {
            InitializeComponent();
            BindingContext = _viewModel= new  NewStockSampleViewModel();
        }

       

        private  void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            string resultText = string.Empty;
            //string userID = "Demo";

            //MSADataBase databaseService
            Device.BeginInvokeOnMainThread(() =>
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