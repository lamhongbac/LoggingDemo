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
    public class NewStockSampleViewModel : BaseViewModel
    {
        private string barcode;
        private string name;
        private string unit;

        public NewStockSampleViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(BarCode)&&
                !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(Unit);
        }
        public string BarCode
        {
            get => barcode;
            set => SetProperty(ref barcode, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
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
            StockSample newItem = new StockSample()
            {
                ID = Guid.NewGuid(),
                BarCode = BarCode.Trim(),
                Name = Name,
                Unit = Unit, CreatedBy = "Demo",ModifiedBy="Demo",
                CreatedDate=DateTime.Now, ModifiedDate= DateTime.Now ,
                 DataState=EDataState.New.ToString(),
            };

         int result=   await MSADataBase.AddStockSample(newItem);

            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
            if (result > 0)
            {
                BarCode = ""; Name = ""; Unit = "";
            }
            else
            {
                
            }
        }
    }
}
