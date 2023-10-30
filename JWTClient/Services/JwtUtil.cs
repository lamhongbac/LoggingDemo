using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.CompilerServices;

namespace JWTClient.Services
{
    public class JwtUtil
    {
        /// <summary>
        /// kiem tra token is Valid truoc khi goi protect API,neu expired thi xin JWT moi
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsValid(string token)
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
        /// <summary>
        /// nhan vao 1 token va pars
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtSecurityToken? ParseToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            // using JwtSecurityToken
            //var jti = tokenS.Claims.First(claim => claim.Type == "jti").Value;
            return tokenS;
        }
    }
}
