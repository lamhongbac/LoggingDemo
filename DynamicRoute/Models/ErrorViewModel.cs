namespace DynamicRoute.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorMessage { get; internal set; }
    }

    public class PageErrorViewModel
    {
        public int StatusCode { get; set; }
        public string OriginalPath { get; set; }
    }
}