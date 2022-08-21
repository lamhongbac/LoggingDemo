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
    public partial class ListOfStockReceivePage : ContentPage
    {
        ListOfStockTransViewModel viewModel;
        public ListOfStockReceivePage()
        {
            InitializeComponent();
            viewModel= new ListOfStockTransViewModel();
            this.BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}