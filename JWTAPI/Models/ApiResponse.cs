namespace JWTAPI.Models
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public JwtData Content { get; internal set; }
    }
}
