namespace DynamicRoute.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class PageErrorViewModel
    {
        public int StatusCode { get; set; }
        public string OriginalPath { get; set; }
    }
}