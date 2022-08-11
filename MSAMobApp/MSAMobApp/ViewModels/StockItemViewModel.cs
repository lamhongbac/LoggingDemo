using MSAMobApp.Data;
using MSAMobApp.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
    /// <summary>
    /// ViewName: StockSampleDetailPage (edit)
    /// View+Edit(Save)
    /// para=id
    /// prop=ID
    /// </summary>
    //[QueryProperty(nameof(ID), "id")]
    public class StockItemViewModel : BaseViewModel
    {
        bool isEdited;
        public bool IsEdited 
        { 
            get => isEdited;
            set {
                SetProperty(ref isEdited, value); 
                Title = "Stock Edit ";
            }
        }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public StockItemViewModel()
        {
            IsEdited = false;
               SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            StockSample editedItem = new StockSample()
            {
                ID = ID,
                BarCode = BarCode.Trim(),
                Name = Name,
                Unit = Unit,               
                ModifiedBy = "Demo"
            };

            int result = await MSADataBase.UpdateAsyncStockSample(editedItem);

            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
            if (result > 0)
            {
                IsEdited = false;
                BarCode = ""; Name = ""; Unit = "";
            }
            else
            {

            }
        }
        private bool ValidateSave()
        {
            return IsEdited && !String.IsNullOrWhiteSpace(BarCode) &&
                !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(Unit);
        }
        private Guid Id;
        public Guid ID
        {
            get => Id;
            set
            {
               
                SetProperty(ref Id, value);
                //LoadItemId(Id);
            }
        }

        public string barCode;
        public string BarCode
        {
            get => barCode;
            set  { SetProperty(ref barCode, value); IsEdited = true; }
           
        }
        public string unit;
        public string Unit
        {
            get => unit;
            set { SetProperty(ref unit, value); IsEdited = true; }
            //set => SetProperty(ref unit, value);
        }
        public string name;
        public string Name
        {
            get => name; set { SetProperty(ref name, value); IsEdited = true; }
            
        }
      
        public async void LoadItemId(Guid itemId)
        {
            try
            {
                var item = await MSADataBase.GetMasterStockItemAsync(itemId);
                if (item != null)
                {
                    BarCode = item.BarCode;
                    ID = item.ID;
                    Name = item.Name;
                    Unit = item.Unit;
                    IsEdited = false;
                }
                else
                {
                    throw new Exception("Failed to Load Item");
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
