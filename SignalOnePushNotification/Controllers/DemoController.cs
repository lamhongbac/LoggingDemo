using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [Route("GetFAQs")]
        [HttpPost]
        public async Task<string> GetFAQs(NotificationMessage model)
        {
            string onesignalUserAuth = "YzI4Mzk5ZGItZmFjNS00OTc3LWFkNjktYjc0YWQ1OTYwNTJh";
            string appID = "64de6e0c-d018-483f-82dd-9aad8123d5d5";
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
        public async Task<string> PushNotification(SinalOneData model)
        {
            string onesignalUserAuth = "YzI4Mzk5ZGItZmFjNS00OTc3LWFkNjktYjc0YWQ1OTYwNTJh";
            string appID = "64de6e0c-d018-483f-82dd-9aad8123d5d5";
            model.app_id = appID;
            Contents contents = new Contents() { en="Lam Hong Bac" };
            model.contents = contents;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("authorization", "Basic " + onesignalUserAuth);
            string json = JsonConvert.SerializeObject(model);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", data);
            return response.Content.ToString();
        }
    }
    /// <summary>
    /// https://onesignal.com/blog/how-to-send-push-notifications-with-the-onesignal-rest-api/
    /// </summary>
    /// <param name="notificationId"></param>
    [HttpGet]
    public void ViewNoti(int notificationId)
    {
        //https://onesignal.com/api/v1/notifications/:id?app_id=:app_id
        const path = string.Format("notifications /"{ 1}?,notificationIdapp_id =${ ONESIGNAL_APP_ID}`;
    }

    public class NotificationMessage
    {
        public string Message { get; set; }
    }
}
