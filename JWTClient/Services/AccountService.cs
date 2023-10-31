using JWTClient.Models;

namespace JWTClient.Services
{
    public class AccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JwtData Login(string username, string password)
        {
            // qua trinh login cua MVC
            //Cap nhat cookie information
        }

        public JwtData ReNewToken(JwtData jwtData)
        {
            //Cap nhat cookie information
        }
    }
}
