using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using JWTAPI.Models;
using System.Security.Claims;
using System.Text.Json;

namespace JWTAPI.Controllers
{
    //https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            LoginInfo user = AuthenticateUser(login);

            if (user != null)
            {
                JwtData jwtData = GenerateJSONWebToken(user);
                user.JwtData= jwtData;
                response = Ok(user);
            }

            return response;
        }
        /// <summary>
        /// Su dung thong tin login de tao json token
        /// khi ve client can parse token thanh information huu ich
        /// hoac la token kg can chua nh thong tin
        /// ma thong tin se nam trong doi tuong loginInfo
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private JwtData GenerateJSONWebToken(LoginInfo userInfo)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userInfo.EmailAddress));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, userInfo.UserID.ToString()));
            claims.Add(new Claim("UserName", userInfo.UserName));
            claims.Add(new Claim("Roles", userInfo.Roles.ToString()));

            foreach (var claimPair in userInfo.CustomsClaims) 
            {

                JsonElement jsonElement =(JsonElement) claimPair.Value;

                var valueType = jsonElement.ValueKind switch
                {
                    JsonValueKind.True => ClaimValueTypes.Boolean,
                    JsonValueKind.False => ClaimValueTypes.Boolean,
                    JsonValueKind.Number => ClaimValueTypes.Double,
                    _ => ClaimValueTypes.String
                };
                var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!,valueType);
                claims.Add(claim);

            }
           
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                 Subject=new ClaimsIdentity (claims),
                 Expires=DateTime.UtcNow.AddMinutes(1),
                 SigningCredentials = credentials,
                 Issuer= _config["Jwt:Issuer"],
                 Audience= _config["Jwt:Issuer"]

            };
            JwtData data = new JwtData();
            //var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            //  _config["Jwt:Issuer"],
            //  null,
            //  expires: DateTime.Now.AddMinutes(120),

            //  signingCredentials: credentials);

            var jwtoken=  jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string jwt= jwtSecurityTokenHandler.WriteToken(jwtoken);
            data.Jwt = jwt;
            return data;
        }

        private LoginInfo AuthenticateUser(UserModel login)
        {
            LoginInfo user = null;

            //Validate the User Credentials
            //Demo Purpose, I have Passed HardCoded User Information
            if (login.Username == "Bac")
            {
                user = new LoginInfo { UserID=Guid.NewGuid(), Username = "Bac", EmailAddress = "lamhong.bac@gmail.com", Roles="admin" };
            }
            return user;
        }
    }
}
