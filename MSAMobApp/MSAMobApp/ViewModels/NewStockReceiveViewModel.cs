﻿using MSAMobApp.Data;
using MSAMobApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
    /// <summary>
    /// model for view scan barcode and receive stock item(s)
    /// </summary>
    public class NewStockReceiveViewModel : BaseViewModel
    {
        public NewStockReceiveViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            //StockTrans = new ObservableCollection<StockTrans>();
            DocNo = UserID+DateTime.Now.ToString("ddmmhhss");
        }
        private string docNo;
        public string DocNo
        {
            get => docNo;
            set => SetProperty(ref docNo, value);
        }
        private string direction = "IN";

        private string shelfCode = "ShDedmo";
        public string ShelfCode
        {
            get => shelfCode;
            set => SetProperty(ref shelfCode, value);
        }
        private string whCode = "WHDemo";
        public string WhCode
        {
            get => whCode;
            set => SetProperty(ref whCode, value);
        }
        private string userID = "DemoUser";
        public string UserID
        {
            get => userID;
            set => SetProperty(ref userID, value);
        }
       
        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value); 
        }
        private string barcode;
        public string BarCode
        {
            get => barcode;
            set => SetProperty(ref barcode, value);
        }
        

        private bool ValidateSave()
        {
            bool validItem = !String.IsNullOrWhiteSpace(BarCode);
            bool isExisted = ExistBarCode(BarCode).Result;
            return validItem && isExisted;
        }

       // public ObservableCollection<StockTrans> StockTrans { get; }
        
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private  void OnSave()
        {

            StockTrans newItem = new StockTrans()
            {
                ID = Guid.NewGuid(),
                BarCode = BarCode,
                Direction = direction,
                Quantity = 1,
                ScanDateTimes = DateTime.Now,
                ShelfCode = shelfCode,
                TCode = ETCode.IR.ToString(),
                UserID = userID,
                WHCode = whCode,
                CreatedBy = userID,
                ModifiedBy = userID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DataState = EDataState.New.ToString(),
            };

            //await MSADataBase.AddStock(newItem);
          //  StockTrans.Add(newItem);

            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
            BarCode = "";

        }

        private async Task<bool> ExistBarCode(string barCode)
        {
            StockSample item=await MSADataBase.GetMasterStockItemAsync(barCode);
            return item != null;
        }
    }
}