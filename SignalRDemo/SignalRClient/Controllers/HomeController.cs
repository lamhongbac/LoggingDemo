using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

using Newtonsoft.Json;
using SignalRClient.Models;
using System.Diagnostics;

namespace SignalRClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HubConnection connection;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            //events-hub
            var connection = new HubConnectionBuilder().WithUrl("https://localhost:7000/api/productoffer/productoffers", options => {
                options.Transports = HttpTransportType.WebSockets;
            }).WithAutomaticReconnect().Build();
            await connection.StartAsync();
            string newMessage1;
            connection.On<object>("receiveEvent", (message) => {
                Console.WriteLine(message); //write in the debug console
                var newMessage = JsonConvert.DeserializeObject<dynamic>(message.ToString());
                newMessage1 = $"{newMessage.chainTip}";

            });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
