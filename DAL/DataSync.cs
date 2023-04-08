﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


/// clientReloads: List{ClientReload} la DS doi tuong chua thong tin ve viec kiem soat synchronize data tu memory va CSDL
/// 
/// ClientReload co cac thuoc tinh:
/// Reload Item: Ten Bang
/// IaInit: Da Init hay chua
/// LastUpdate: Ngay cap nhat data cuoi cung
/// InitCycleDays: la cycle de thuc hien re-init lai
///
///
namespace DAL
{
    /// <summary>
    /// SyncManagement class
    /// 
    /// lop theo doi synchronize
    /// +lan update cuoi cung
    /// +lan isnit cuoi cung
    /// +da init chua
    /// </summary>
    public class DataSync
    {
        /// <summary>
        /// thuc hien kiem tra 1 item da init hay chua
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool IsInit(EReload tableName)
        {
            var existItem = clientReloads.Where(x => x.ClientReloadItem.TableName == tableName.ToString()).FirstOrDefault();
            if (existItem!=null)
            {
                return existItem.IsInit;
            }
            else
            { return false; }
        }
        public DataSync()
        {
            clientReloads = new List<ClientReload>();
        }
        List< ClientReload> clientReloads;

        /// <summary>
        /// thuc hien sau khi init 1 bang
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="initDate"></param>
        public void UpdateInitDate(EReload tableName,DateTime initDate)
        {
            var existItem = clientReloads.Where(x => x.ClientReloadItem.TableName == tableName.ToString()).FirstOrDefault();
            if (existItem != null)
            {
                existItem.LastInitDate = initDate;
                existItem.IsInit = true;
            }
            else
            {
                ClientReloadData item = new ClientReloadData()
                {
                    LastUpdated = initDate,
                    TableName = tableName.ToString(),
                };

                clientReloads.Add(new ClientReload()
                {
                    ClientReloadItem = item,
                    InitCycleDays = 7,
                    IsInit = true,
                    LastInitDate = initDate

                });

            }
            
        }
        public void UpdateLastUpdateDate(EReload tableName, DateTime _lastUpdated)
        {
            var existItem = clientReloads.Where(x => x.ClientReloadItem.TableName == tableName.ToString()).FirstOrDefault();
            if (existItem != null)
            {
                existItem.ClientReloadItem.LastUpdated= _lastUpdated;
            }

        }
        public DateTime GetLastUpdateDate(EReload tableName)
        {
            var existItem = clientReloads.Where(x => x.ClientReloadItem.TableName == tableName.ToString()).FirstOrDefault();
            if (existItem != null)
            {
                return existItem.ClientReloadItem.LastUpdated;
            }
            else throw new Exception("Miss tracking, check init module");

        }
    }
    /// <summary>
    /// Init_cycle_days so ngay phai init lai
    /// LastInit_date: ngay thuc hien Init cuoi cung
    /// </summary>
    public class ClientReload
    {
        public ClientReload()
        {
            ClientReloadItem = new ClientReloadData();
        }
        public ClientReloadData ClientReloadItem { get; set; }
        public bool IsInit { get; set; }
        public DateTime LastInitDate { get;set; }
        public int InitCycleDays { get; set; }
    }
    /// <summary>
    /// du lieu tuong ung G_ClientReload
    /// </summary>
    public class ClientReloadData
    {
        public int ID { get; set; }
        public string TableName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
    public enum EReload
    {
        Outlet, //G_Outlet
        Brand, //G_Brand
        Branch, //G_Branch
        News, //HQ_News
    }
}
