using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneSignalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoOneSignalController : ControllerBase
    {
        [HttpPost]
        [Route("PushNotification")]
        public async Task<IActionResult> PushNotification(string message)
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
                new KeyValuePair<string, string>("contents[en]",message ),
                new KeyValuePair<string, string>("included_segments", "All")
            });

            var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
            return Ok(response);
        }
    }
}
