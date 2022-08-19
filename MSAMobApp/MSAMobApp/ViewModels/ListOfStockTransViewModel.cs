using MSAMobApp.Data;
using MSAMobApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
 public   class ListOfStockTransViewModel: BaseViewModel
    {
        public Command LoadItemsCommand { get; }
        public ListOfStockTransViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ListStockTransViewModels = new ObservableCollection<StockTrans>();
        }
        public ObservableCollection<StockTrans> ListStockTransViewModels { get; set; }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                ListStockTransViewModels.Clear();
                List<StockTrans> items = await StockTransDatabase.GetStockTrans(); ;
                foreach (var item in items)
                {                    
                    ListStockTransViewModels.Add(item);
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
    }
}
