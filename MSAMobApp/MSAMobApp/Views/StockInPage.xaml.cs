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
    public partial class StockInPage : ContentPage
    {
        StockTransViewModel _viewModel;
        public StockInPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new StockTransViewModel();
        }

        private void scanResultText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}