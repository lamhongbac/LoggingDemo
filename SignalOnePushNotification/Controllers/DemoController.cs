using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneSignalApi.Api;
using OneSignalApi.Client;
using OneSignalApi.Model;
using SignalOnePushNotification.Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignalOnePushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //string   ONESIGNAL_APP_ID=
        string ONESIGNAL_APP_ID = "64de6e0c-d018-483f-82dd-9aad8123d5d5";
        string onesignalUserAuth = "YzI4Mzk5ZGItZmFjNS00OTc3LWFkNjktYjc0YWQ1OTYwNTJh";

        [Route("GetFAQs")]
        [HttpPost]
        public async Task<string> GetFAQs(NotificationMessage model)
        {

            string appID = ONESIGNAL_APP_ID;
            //string message = "";
            //EditorMessage.Text.Trim()

            //SecureStorage.GetAsync("AppID").Result
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://onesignal.com/api/v1/notifications");
            client.DefaultRequestHeaders.Add("authorization", "Basic " + onesignalUserAuth);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("app_id",appID ),
                new KeyValuePair<string, string>("contents[en]",model.Message ),
                new KeyValuePair<string, string>("included_segments", "All")
            });

            var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
            return model.Message;
        }
        /// <summary>
        /// https://onesignal.com/blog/our-net-api-library-is-now-available/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> PushNotification(SinalOneData model)
        {
            string onesignalUserAuth = "YzI4Mzk5ZGItZmFjNS00OTc3LWFkNjktYjc0YWQ1OTYwNTJh";
            var appConfig = new Configuration();
            appConfig.BasePath = "https://onesignal.com/api/v1";
            appConfig.AccessToken = onesignalUserAuth;

           
            var appInstance = new DefaultApi(appConfig);
            Notification notification = new Notification(appId: ONESIGNAL_APP_ID)
            {
                Contents = new StringMap(en: "Hello World from .NET!"),
                IncludedSegments = new List<string> { "Subscribed Users" }
            };
            var response = await appInstance.CreateNotificationAsync(notification);
            string message = "Notification created for {response.Recipients} recipients";
            return message;

            //model.app_id = ONESIGNAL_APP_ID;
            //Contents contents = new Contents() { en = "Lam Hong Bac" };
            //model.contents = contents;

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("authorization", "Basic " + onesignalUserAuth);
            //string json = JsonConvert.SerializeObject(model);
            //StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", data);
            //return response.Content.ToString();
        }

        /// <summary>
        /// https://onesignal.com/blog/how-to-send-push-notifications-with-the-onesignal-rest-api/
        /// </summary>
        /// <param name="notificationId"></param>
        [HttpGet]
        public void ViewNotification(int notificationId)
        {
            //https://onesignal.com/api/v1/notifications/:id?app_id=:app_id
             string path = string.Format("notifications/? {0} ?app_id={1}", notificationId, ONESIGNAL_APP_ID);
        }

        public class NotificationMessage
        {
            public string Message { get; set; }
        }
    }
}
