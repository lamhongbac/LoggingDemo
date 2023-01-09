using PushNotification.Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Helper.Common
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            OneSignalConfig = new OneSignalConfiguration();

        }
        public  string ConnectionString { get; set; }
        public OneSignalConfiguration OneSignalConfig { get; set; }
    }
}
