using Dapper;
using NotificationDEMO.Common;
using NotificationDEMO.Interfaces;
using NotificationDEMO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationDEMO.Services
{
    public class NotificationService : INotiService
    {
        public NotificationService()
        {

        }
        List<Noti> _notifications = new List<Noti>();
        public List<Noti> GetNotifications(int toUserID, bool isUnread = false)
        {
            try
            {
                string conn = Global.ConnectionString;

                using (IDbConnection connection = new SqlConnection(Global.ConnectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    string sql = @"SELECT * FROM V_WebNotification Where ToUserId=" + toUserID.ToString();
                    var result = connection.Query<Noti>(sql);
                    List<Noti> list = result.ToList();
                    if (list != null && list.Count > 0)
                    {
                        return list.Where(x => x.IsRead == isUnread).ToList(); ;
                    }
                    return new List<Noti>();
                }


            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }

        }
    }
}
