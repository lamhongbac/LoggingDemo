using MSAMobApp.Data;
using MSAMobApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MSAMobApp.ViewModels
{
    /// <summary>
    /// model for view scan barcode and receive/Issue/Tranfer stock item(s)
    /// </summary>
    public class StockTransViewModel : BaseViewModel
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public StockTransViewModel()
        {
            SaveCommand = new Command( OnSave);
            CancelCommand = new Command(OnCancel);
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            StockTransDetailCol = new ObservableCollection<StockTransItemViewModel>();
            UserID = "DemoUser";
            DocNo = UserID+DateTime.Now.ToString("ddmmhhss");
            ID = Guid.NewGuid();
            StockTransDetails = new List<StockTransDetail>();
            Quantity = 1;
            MinDate = DateTime.Now.AddDays( - 10);
            MaxDate = DateTime.Now;
            TransDate = DateTime.Now;
        }

        internal bool ExistBarCode(string scanedBarCode)
        {
            return StockTransDetailCol.Where(x => x.BarCode == scanedBarCode).FirstOrDefault() != null;
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

        private string notes = "demo transaction";
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
        private string unit;
        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
        }
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }
        #endregion
        private bool ValidateAddItem()
        {            
            bool isExisted = ExistBarCode(ScanedBarCode);
            return !string.IsNullOrWhiteSpace(ScanedBarCode) && !isExisted;
        }
        private List<StockTransDetail> StockTransDetails;
        ObservableCollection<StockTransItemViewModel> stockTransDetailCol;
        public ObservableCollection<StockTransItemViewModel> StockTransDetailCol 
        { 
            get=> stockTransDetailCol;
            set => SetProperty(ref stockTransDetailCol, value);
        }
        
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        //public Command LoadItemsCommand { get; }
        
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        /// <summary>
        /// save to local DB
        /// </summary>
        private async  void OnSave()
        {
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
                Description=Notes, 
                Number=DocNo, 
                StoreNumber=WhCode, 
                TransDate=DateTime.Now,
                StockTransDetails = StockTransDetails,
            };
          await  StockTransDatabase.CreateStockTrans(stockTrans);
 
        }


        /// <summary>
        /// Button add one just ScanBarCode item to list
        /// </summary>
        public void AddDetail()
        {
            StockTransDetail stockTransDetail = new StockTransDetail()
            {
                ID = Guid.NewGuid(),
                StockTransID = this.ID,
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
            StockTransDetails.Add(stockTransDetail);
            StockTransItemViewModel item = new StockTransItemViewModel(stockTransDetail,Name,Unit);

            //await MSADataBase.AddStock(newItem);
            StockTransDetailCol.Add(item);
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
