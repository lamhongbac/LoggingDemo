﻿using MSAMobApp.DataBase;
using MSAMobApp.Models;
using MSAMobApp.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MSAMobApp.Services
{
    public class XAppContext
    {
        public string DeviceID => deviceIdentifier;
        const string StockMasterItemSyncDate = "StockMasterItemSyncDate";
        public  MobUser LoginedUser;//login ID
        static XAppContext instance;
         DateTime LoginDate;
        CompanyInfo CompanyLogined;
        string deviceIdentifier;
        
        private string defaultShelfCode;
        private  string storeNumber;
        public  string StoreNumber => storeNumber;
        public string DefaultShelfCode => defaultShelfCode;
        public  string UserID => LoginedUser.UserID;

        //login thanh cong goi function nay
        public  void SetLogin(MobUser loginedUser)
        {
            LoginedUser = loginedUser;
            LoginDate = DateTime.Now;
        }
        XAppContext()
        {
            IDevice device = DependencyService.Get<IDevice>();
            deviceIdentifier = device.GetIdentifier();
            if (string.IsNullOrEmpty(deviceIdentifier))
            {
                deviceIdentifier = "DemoDevice";
            }
            defaultShelfCode = "DemoShelf";
        }
        public static XAppContext GetInstance()
        {
            if (instance == null)
            {
                instance = new XAppContext();
                instance.LoadContext();


            }
            return instance;
        }
        /// <summary>
        /// Local luu giu cac gia tri vao thiet bi
        /// Ham nay doc cac gia tri cau hinh dua vao appcontext dam bao app phan anh dung thuc trang truoc khi di ngủ
        /// 
        /// </summary>
        private void LoadContext()
        {
            //Read last value from DB


            CompanyLogined = new CompanyInfo();
            CompanyLogined.Address = "09-11 D52 Tan Binh District HCM city";
            CompanyLogined.Name = "MSA AppTech Co.";
            CompanyLogined.Email = "lamhong.bac@ms-apptech.com";
            CompanyLogined.CEOName = "Lam Hong Bac";
            CompanyLogined.BusinessType = "Unknow";
            CompanyLogined.ID = 112233;
            CompanyLogined.MobileNo = "0913660575";

            LoginedUser = new MobUser()
            {
                UserID = "DemoUser",
                Pwd = "*****"
            };
            LoginDate = DateTime.Now;
            storeNumber = "DemoStore";
            //load master data

        }
        /// <summary>
        /// Ham nay thuc hien luu context khi thoat App hoac khi App In -Active
        /// </summary>
        public void SaveContext()
        {
            //Save to Local DB
        }

        //gia lap user login tai cua hang
        public DateTime LastStockMasterItemSyncDate { get => GetMasterDataSyncDate(); }
        private DateTime GetMasterDataSyncDate()
        {
            var default_val = DateTime.MinValue.ToString();

            var myValue = Preferences.Get(StockMasterItemSyncDate, default_val);

            return Convert.ToDateTime(myValue);

        }
        public void SaveMasterDataSyncDate(DateTime syncDate)
        {
            Preferences.Set(StockMasterItemSyncDate, syncDate.ToString());
        }
        
        public string GLocation { get; set; } //dia chi cua thiet bi
        public string HID { get => deviceIdentifier; } //HID cua thiet bi
    }
}
