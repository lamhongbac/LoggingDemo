using System.IdentityModel.Tokens.Jwt;
namespace JWTClient.Services
{
    public class JwtUtil
    {
        /// <summary>
        /// kiem tra token is Valid truoc khi goi protect API,neu expired thi xin JWT moi
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
    }
}
