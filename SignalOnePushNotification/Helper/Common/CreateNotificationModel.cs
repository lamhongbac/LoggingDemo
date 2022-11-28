using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalOnePushNotification.Helper.Common
{
    public class CreateNotificationModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> PlayerIds { get; set; }
    }
}
