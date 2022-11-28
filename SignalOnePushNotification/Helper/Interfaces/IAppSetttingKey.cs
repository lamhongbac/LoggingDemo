using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalOnePushNotification.Helper.Interfaces
{
   public interface IAppSetttingKey
    {
         string AppID { get; set; }
         string AppKey { get; set; }
    }
}
