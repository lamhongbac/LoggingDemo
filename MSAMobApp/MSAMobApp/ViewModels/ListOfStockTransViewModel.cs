
using MSAMobApp.DataBase;
using MSAMobApp.Services;
using MSAMobApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using MSAMobApp.Models;
using MSAMobApp.Data;
using Acr.UserDialogs;

namespace MSAMobApp.ViewModels
{
 public   class ListOfStockTransViewModel: BaseViewModel
    {
        XAppContext appContext;
        public Command StockReceiveCommand { get; }
        public Command SyncStockCommand { get; }
        
        public Command LoadItemsCommand { get; }
        public ListOfStockTransViewModel()
        {
            appContext = XAppContext.GetInstance();
               LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            StockReceiveCommand = new Command(async () => await GoToReceiveStockPage());
            SyncStockCommand = new Command(async () => await ExecuteSyncItemsCommand());
            DataList = new ObservableCollection<StockTrans>();
        }
        //await Shell.Current.GoToAsync(nameof(NewStockItem));
        public ObservableCollection<StockTrans> DataList { get; set; }
        async Task GoToReceiveStockPage()
        {
          await  Shell.Current.GoToAsync(nameof(StockReceivePage));
        }
        /// <summary>
        /// Sync Local Data to remote Server
        /// </summary>
        /// <returns></returns>
        async Task ExecuteSyncItemsCommand()
        {
            List<StockTrans> stockTrans =await MSADataBase.GetLocalStockTrans(EDataState.New);
            if (stockTrans==null || stockTrans.Count==0)
            {
                await UserDialogs.Instance.AlertAsync("No item to sync");

                return;
            }
            bool success = await MSADataBase.SyncLocalStockTrans(stockTrans);
            if (!success)
            {
              await  UserDialogs.Instance.AlertAsync("Sync to DB fail");
            }
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            string TCode = EWHMTCode.SR.ToString();
            try
            {
                DataList.Clear();
                DateTime now = DateTime.Now.Date;
                GetStockTransModel model = new GetStockTransModel()
                {
                    FromDate = now,
                    ToDate = now,
                    StoreNumber = appContext.StoreNumber
                };

                List<StockTrans> items = await StockTransDBService.GetStockTrans(model);
                if (items == null || items.Count == 0)
                {
                    return;
                }

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
                string error = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
