using MSAMobApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MSAMobApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}