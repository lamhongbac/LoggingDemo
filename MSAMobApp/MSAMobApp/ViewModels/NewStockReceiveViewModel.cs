using MSAMobApp.Data;
using MSAMobApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
    /// <summary>
    /// model for view scan sample barcode
    /// </summary>
    public class NewStockReceiveViewModel : BaseViewModel
    {
        private string barcode;
        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value); 
        }
        public NewStockReceiveViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(BarCode);
                
        }
        public string BarCode
        {
            get => barcode;
            set => SetProperty(ref barcode, value);
        }
        

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            StockTrans newItem = new StockTrans()
            {
                ID = Guid.NewGuid(),
                BarCode = BarCode,
                Direction = "In", Quantity = 1, ScanDateTimes = DateTime.Now, 
                SelfCode = "SDedmo",
                TCode = ETCode.IR.ToString(), UserID="Demo", WHCode="WHDemo",
                CreatedBy = "Demo",ModifiedBy="Demo",
                CreatedDate=DateTime.Now, ModifiedDate= DateTime.Now ,
                 DataState=EDataState.New.ToString(),
            };

            await MSADataBase.AddStock(newItem);

            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
            BarCode = ""; Name = ""; Unit="";

        }
    }
}
