namespace JWTAPI.Models
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Content { get; set; }
    }
}
