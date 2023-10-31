using System.Security.Claims;

namespace JWTAPI.Models
{
    public class LoginInfo
    {
        public DateTime LoginDate { get; set; }
        public LoginInfo()
        {
            
            JwtData=new JwtData();
            
        }
       

        //example cho truong hop user data tong quat duoi dang string object
        
        public JwtData JwtData { get; set; }
    }
}
