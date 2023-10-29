using System.Security.Claims;

namespace JWTAPI.Models
{
    public class LoginInfo
    {
        public LoginInfo()
        {
            Roles = "User";
            JwtData=new JwtData();
            CustomsClaims = new Dictionary<string, object>();
        }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Roles { get; set; }

        //example cho truong hop user data tong quat duoi dang string object
        public Dictionary<string,object> CustomsClaims { get; set; }
        public JwtData JwtData { get; set; }
    }
}
