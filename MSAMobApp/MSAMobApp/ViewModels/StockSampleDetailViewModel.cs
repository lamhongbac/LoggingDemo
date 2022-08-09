using MSAMobApp.Data;
using MSAMobApp.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
    [QueryProperty(nameof(ID), nameof(ID))]
    public class StockSampleDetailViewModel : BaseViewModel
    {
        private Guid Id;
        public Guid ID
        {
            get => Id;
            set
            {
                SetProperty(ref Id, value);
                LoadItemId(Id);
            }
        }

        public string barCode;
        public string BarCode
        {
            get => barCode;
            set => SetProperty(ref barCode, value);
        }
        public string unit;
        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
        }
        public string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
      
        public async void LoadItemId(Guid itemId)
        {
            try
            {
                var item = await MSADataBase.GetMasterStockItemAsync(itemId);
                ID = item.ID;
                Name = item.Name;
                Unit = item.Unit;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
