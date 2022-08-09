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
    public partial class StockSampleDetailPage : ContentPage
    {
        public StockSampleDetailPage()
        {
            StockSampleDetailViewModel _viewModel;
            InitializeComponent();
            BindingContext = _viewModel=new StockSampleDetailViewModel();
        }
    }
}