using MSAMobApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/// <summary>
/// StockMaster-> cac new barcode se dc gan ten va load len CSDL voi trang thai data= new
/// danh sach cac sample barcode da scan load tu CSDL len
/// tu danh sach nay ta co the edit 1 sample or create(scan) new sample
/// </summary>
namespace MSAMobApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockMasteMaintainPage : ContentPage
    {
        StockSamplesViewModel _viewModel;
        public StockMasteMaintainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel=new StockSamplesViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}