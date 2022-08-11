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
    [QueryProperty(nameof(ID), "StockID")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockItemPage : ContentPage
    {
        public StockItemPage()
        {
            InitializeComponent();
        }
        public string ID
        {

            set
            {
                StockItemViewModel _viewModel;
                _viewModel = new StockItemViewModel();
                Guid newGuid = Guid.Parse(value);
                _viewModel.LoadItemId(newGuid);
                BindingContext = _viewModel;
            }
        }
    }
}