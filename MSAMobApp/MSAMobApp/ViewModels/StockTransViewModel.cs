using MSAMobApp.Data;
using MSAMobApp.DataBase;
using MSAMobApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MSAMobApp.Models;

namespace MSAMobApp.ViewModels
{
    /// <summary>
    /// model for view scan barcode and receive/Issue/Tranfer stock item(s)
    /// </summary>
    public class StockTransViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command AddDetailCommand { get; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        private string hid;
        public StockTransViewModel()
        {
            hid = App.AppContext.HID;

                SaveCommand = new Command( OnSave);
            CancelCommand = new Command(OnCancel);
            AddDetailCommand = new Command(AddDetail);
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            StockTransDetailCol = new ObservableCollection<StockTransItemViewModel>();
            //StockTransDetails = new List<StockTransDetail>();
            Reset();
        }
        void Reset()
        {
            UserID = "DemoUser";
            DocNo = UserID + DateTime.Now.ToString("ddmmhhss");
            ID = Guid.NewGuid();
            //StockTransDetails.Clear();
            Quantity = 1;
            MinDate = DateTime.Now.AddDays(-10);
            MaxDate = DateTime.Now;
            TransDate = DateTime.Now;
            StockTransDetailCol.Clear();
        }
        internal StockTransItemViewModel ExistBarCode(string scanedBarCode)
        {
            return StockTransDetailCol.Where(x => x.BarCode == scanedBarCode).FirstOrDefault();
        }
        #region prop
        private Guid id;
        public Guid ID
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string docNo;
        public string DocNo
        {
            get => docNo;
            set => SetProperty(ref docNo, value);
        }
        //private string direction = "IN";
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
        private DateTime transDate;
        public DateTime TransDate { 
            get => transDate; 
            set => SetProperty(ref transDate, value); 
        }

        private string notes = "demo transaction "+DateTime.Now.ToString("dd/MM/yy hhmm");
        public string Notes
        {
            get => notes;
            set => SetProperty(ref notes, value);
        }

        #endregion

        #region scan barCode
        private string barcode;
        public string ScanedBarCode
        {
            get => barcode;
            set => SetProperty(ref barcode, value);
        }
        //private string unit;
        //public string Unit
        //{
        //    get => unit;
        //    set => SetProperty(ref unit, value);
        //}
        //private string name;
        //public string Name
        //{
        //    get => name;
        //    set => SetProperty(ref name, value);
        //}
        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }
        #endregion
        private bool ValidateAddItem()
        {            
            bool isExisted = ExistBarCode(ScanedBarCode)!=null;
            return !string.IsNullOrWhiteSpace(ScanedBarCode) && !isExisted;
        }
        
        ObservableCollection<StockTransItemViewModel> stockTransDetailCol;
        public ObservableCollection<StockTransItemViewModel> StockTransDetailCol 
        { 
            get=> stockTransDetailCol;
            set => SetProperty(ref stockTransDetailCol, value);
        }
        
        
        //public Command LoadItemsCommand { get; }
        
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// save to local DB
        /// </summary>
        private async void OnSave()
        {
            List<StockTransDetail> StockTransDetails=new List<StockTransDetail>();
            foreach (var item in StockTransDetailCol)
            {
                //StockTransDetail transDetail = new StockTransDetail()
                //{ CreatedOn = item.CreatedOn, CreatedBy = item.CreatedBy,
                //    BarCode = item.BarCode, DataState = EDataState.New.ToString(),
                //    ID = Guid.NewGuid(), ItemNumber=item.Number, ModifiedBy=item.ModifiedBy,
                //     ModifiedOn=item.ModifiedOn, Quantity=item.Quantity, ScanDateTimes=item.ScanDateTimes,
                //      TransID= ID

                //};
                StockTransDetails.Add(item.TransDetail);
            }
            StockTrans stockTrans = new StockTrans()
            {
                ID = ID,
                ShelfCode = shelfCode,
                TCode = EWHMTCode.SR.ToString(), //receive 
                UserID = userID,
                CreatedBy = userID,
                ModifiedBy = userID,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                DataState = EDataState.New.ToString(),
                Description = Notes,
                Number = DocNo,
                StoreNumber = XAppContext.StoreNumber,
                TransDate = DateTime.Now,
                StockTransDetails = StockTransDetails,
                GLocation = "11000;87654",
                HID = hid,

        };
            bool ret = await MSADataBase.CreateStockTrans(stockTrans);
            if (ret)
                Reset();
        }


        /// <summary>
        /// Button add one just ScanBarCode item to list
        /// </summary>
        public async void AddDetail()
        {
            string scanedBarCode = ScanedBarCode.Trim();
            StockTransItemViewModel find_stockTransDetail=ExistBarCode(scanedBarCode);
            
            //neu da add roi thi chi tang so luong
            if (find_stockTransDetail != null)
            {
                find_stockTransDetail.Quantity += Quantity;
                ScanedBarCode = ""; Quantity = 1;
                return;
            }

            //else part
            MobStockMasterItem item = await MSADataBase.GetMasterStockItemAsync(scanedBarCode);
            string name;
            string unit;
            if (item != null)
            {
                unit = item.Unit;
                name = item.Name;
                //_viewModel.AddDetail();
            }
            else
            {
                return;
            }
            StockTransDetail stockTransDetail = new StockTransDetail()
            {
                ID = Guid.NewGuid(),
                TransID = this.ID,
                BarCode = ScanedBarCode,
                ItemNumber = ScanedBarCode, 
                Quantity=Quantity, 
                ScanDateTimes=DateTime.Now,
                CreatedBy = userID,
                ModifiedBy = userID,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                DataState = EDataState.New.ToString(),

            };
            //StockTransDetails.Add(stockTransDetail);
            StockTransItemViewModel stkviewModel = new StockTransItemViewModel(stockTransDetail,name,unit);

            //await MSADataBase.AddStock(newItem);
            StockTransDetailCol.Add(stkviewModel);
            ScanedBarCode = ""; Quantity = 1;
        }
        private async Task<MobStockMasterItem> ExistItem(string barCode)
        {
            MobStockMasterItem item=await MSADataBase.GetMasterStockItemAsync(barCode);
            return item;
            //return true;
        }


    }
}
