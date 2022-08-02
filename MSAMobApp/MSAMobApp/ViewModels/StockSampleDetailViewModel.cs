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
        public Guid ID { get; set; }
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
                var item = await MSADataBase.GetStockISampletemAsync(itemId);
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
