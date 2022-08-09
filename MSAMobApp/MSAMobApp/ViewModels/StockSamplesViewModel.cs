using MSAMobApp.Data;
using MSAMobApp.Models;
using MSAMobApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
    /// <summary>
    ///  Danh sach barcode da save vao CSDL
    ///  cung cap cho view StockMaster Maintain
    /// </summary>
    public class StockItemsViewModel : BaseViewModel
    {
        private StockSample _selectedItem;

        public ObservableCollection<StockSample> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<StockSample> ItemTapped { get; }

        public StockItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<StockSample>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<StockSample>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await MSADataBase.GetStockSamples();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public StockSample SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewStockItem));
        }

        async void OnItemSelected(StockSample item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(StockSampleDetailPage)}?{nameof(StockSampleDetailViewModel.ID)}={item.ID}");
        }
    }
}