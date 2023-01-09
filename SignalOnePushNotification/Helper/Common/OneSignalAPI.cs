using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalOnePushNotification.Helper.Common
{
    public class OneSignalAPI
    {
        public const string BASE_URL = "https://onesignal.com/api/v1/notifications";
        public const string API_KEY = "YzI4Mzk5ZGItZmFjNS00OTc3LWFkNjktYjc0YWQ1OTYwNTJh";//"YOUR ONESIGNAL API KEY";
        public const string ONESIGNAL_APP_ID = "64de6e0c-d018-483f-82dd-9aad8123d5d5";// "YOUR ONESIGNAL APP ID";
        //const BASE_URL = "https://onesignal.com/api/v1";
    }
    public class Contents
    {
        public string en { get; set; }
    }

    public class Data
    {
        public string foo { get; set; }
    }

    public class SinalOneData
    {
        public string app_id { get; set; }
        public List<string> included_segments { get; set; }
        public Data data { get; set; }
        public Contents contents { get; set; }
    }
    //return type

    //    {
    //  "errors": [
    //    "app_id not found. You may be missing a Content-Type: application/json header."
    //  ]
    //}
}
