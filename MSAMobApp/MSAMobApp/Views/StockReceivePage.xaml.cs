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
        NewStockReceiveViewModel _viewModel;
        public StockReceivePage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new NewStockReceiveViewModel();
        }
    }
}