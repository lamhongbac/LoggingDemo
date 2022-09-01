using MSAMobApp.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
  public  class StockTransItemViewModel: BaseViewModel
    {
        public Command AddQtyCommand { get; }
        public Command RemoveQtyCommand { get; }
        public Guid ID { get; set; }

        StockTransDetail _detailItem;
        public StockTransDetail TransDetail => _detailItem;
        public Guid StockTransID { get => _detailItem.ID; } //khoa ngoai
        
        public string BarCode { get => _detailItem.BarCode; } //dung ra la phai chi dinh khoa ngoai vao bang master stock Item
        //public string Number { get; set; } //ma item
        public string Name { get; set; } //ten Item
        public string Unit { get; set; } //DVT Item
        public DateTime ScanDateTimes { get; set; }
        //public int Quantity { get => _detailItem.Quantity; set => _detailItem.Quantity = value; }
        int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); _detailItem.Quantity = value; }
        }
        //public string DataState { get; set; } //data state de du tru cho viec thay doi tren 1  dòng
        //public DateTime CreatedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        //public DateTime ModifiedOn { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        //public string CreatedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        //public string ModifiedBy { get; set; }//data  de du tru cho viec thay doi tren 1  dòng
        public StockTransItemViewModel(StockTransDetail detailItem, string name, string unit)
        {
            _detailItem = new StockTransDetail();
               _detailItem = detailItem;
              // ID = detailItem.ID;
            //StockTransID = detailItem.TransID;
            //BarCode = detailItem.BarCode;
            Quantity = detailItem.Quantity;
            //Number = detailItem.ItemNumber;
            //CreatedBy = detailItem.CreatedBy;
            //ModifiedBy = detailItem.ModifiedBy;
            //CreatedOn = DateTime.Now;
            //ModifiedOn = DateTime.Now;
            //DataState = EDataState.New.ToString();
            Name = name;
            Unit = unit;
            AddQtyCommand = new Command(OnAddQty);
            RemoveQtyCommand = new Command(OnRemoveQty);
        }
        public StockTransItemViewModel()
        {
            AddQtyCommand = new Command(OnAddQty);
            RemoveQtyCommand = new Command(OnRemoveQty);
        }
        private  void OnAddQty()
        {
            Quantity += 1;
        }
        private  void OnRemoveQty()
        {
            if (Quantity > 0)
                Quantity -= 1;
        }
    }
}
