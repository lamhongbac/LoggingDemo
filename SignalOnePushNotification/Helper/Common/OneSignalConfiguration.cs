using SignalOnePushNotification.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotification.Helper.Common
{
    public class OneSignalConfiguration: IAppSetttingKey
    {
        public string AppID { get; set; }
        public string AppKey { get; set; }
    }
}
