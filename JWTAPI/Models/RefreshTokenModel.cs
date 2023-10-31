namespace JWTAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RefreshTokenModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } //link den bang User
        

        public string Token { get; set; }
        public string JwtId { get; set; } //truy van Id cua Jwt

        public bool IsUsed { get; set; } //ngay su dung
        public bool IsRevoked { get; set; } // ngay huy bo

        public DateTime ExpiredAt { get; set; } 
        public DateTime IssuedAt { get; set;}
    }
}
