using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MVCWeb.Models
{
    public class SendEmailTask : BackgroundService
    {
        ILogger<SendEmailTask> _logger;
        public SendEmailTask(ILogger<SendEmailTask> logger)
        {
            _logger= logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           await SendEmail();
        }
        
        public async Task<bool> SendEmail()
        {
           for (int i=0; i < 10; i++) 
            { 
                //send email
            }
           return true;
        }
    }
    public class UpdateSQLTask : BackgroundService
    {
        ILogger<UpdateSQLTask> _logger;
        public UpdateSQLTask(ILogger<UpdateSQLTask> logger) { _logger = logger; }
        public string SQLCommand { get; set; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
          await  UpdateSQL(SQLCommand);
        }

        public async Task  UpdateSQL(string SQLCommand)
        {
             await Task.CompletedTask; ;
        }
    }
}
