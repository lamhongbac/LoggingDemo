using Microsoft.Extensions.Logging;
using System;

namespace MVCWeb.Models
{
    public class ErrorViewModel
    {
        private readonly ILogger<ErrorViewModel> _logger;
        public ErrorViewModel(ILogger<ErrorViewModel> logger)
        {
            _logger = logger;
        }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
