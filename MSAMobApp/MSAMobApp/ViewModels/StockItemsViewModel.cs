using MSAMobApp.Data;
using MSAMobApp.Models;
using MSAMobApp.Services;
using MSAMobApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
    /// <summary>
    /// ViewName:StockMasteMaintainPage
    ///  Danh sach barcode da save vao CSDL
    ///  cung cap cho view StockMaster Maintain
    /// </summary>
    public class StockItemsViewModel : BaseViewModel
    {
        private MobStockMasterItem _selectedItem;

        public ObservableCollection<MobStockMasterItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SyncItemsCommand { get; }
        public Command<MobStockMasterItem> ItemTapped { get; }

        public StockItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<MobStockMasterItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<MobStockMasterItem>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            SyncItemsCommand = new Command(OnSyncItems);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await MSADataBase.GetStockMasterItems();
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

        public MobStockMasterItem SelectedItem
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
        //OnSyncItems
        private async void OnSyncItems(object obj)
        {
            StockMasterService stockMasterService = new StockMasterService();
            stockMasterService.CreateStockItems(items)
        }
        async void OnItemSelected(MobStockMasterItem item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            
            string id = item.ID.ToString();

            await Shell.Current.GoToAsync($"{nameof(StockItemPage)}?{"StockID"}={id}");
        }
    }
}