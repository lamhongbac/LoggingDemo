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
            DataList = new ObservableCollection<StockTrans>();
        }
        public ObservableCollection<StockTrans> DataList { get; set; }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                DataList.Clear();
                List<StockTrans> items = await MSADataBase.GetStockTrans(); ;
                foreach (var item in items)
                {                    
                    DataList.Add(item);
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
        public StockTrans SelectedItem { get; set; }
        internal void OnAppearing()
        {
            try
            {
                IsBusy = true;
                SelectedItem = null;
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
