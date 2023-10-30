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
using System.Security.Cryptography;

namespace JWTAPI.Controllers
{
    //https://jasonwatmore.com/net-6-jwt-authentication-with-refresh-tokens-tutorial-with-example-api?fbclid=IwAR2Kr20boun2Py70sg7qApMf5WmzwoIRP6_qV64Ic1XDcokHIlRuX37R3Ss_aem_ATZzWQpbOzlNTC0WiiWdxSP4w0Uxc2PTS-6BODOm8PO1GZ2pNwePogDX1s6KDxgIIf4
    //https://www.youtube.com/watch?v=AQwS4-5YV4o
    //https://www.youtube.com/watch?v=mgeuh8k3I4g
    //https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        RefreshTokenDatas _tokenDatas;
        public LoginController(IConfiguration config, RefreshTokenDatas tokenDatas)
        {
            _config = config;
            _tokenDatas = tokenDatas;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            LoginInfo? user = AuthenticateUser(login);

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
            string refreshToken = GenerateRefreshToken();

            //save data to DB
            RefreshTokenModel refreshTokenModel = new RefreshTokenModel()
            {
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(10),

                Id = Guid.NewGuid(),
                IsRevoked = false,               
                IsUsed = false,

                JwtId = jwtoken.Id,

                Token = refreshToken,
                UserId = userInfo.UserID
            };
            //save token
            _tokenDatas.AddToken(refreshTokenModel);

            data.Jwt = jwt;
            data.RefreshToken = refreshTokenModel.Token;

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult IsValidToken(JwtData model)
        {
            JwtSecurityTokenHandler tokenSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtConfig config= _config.GetSection("Jwt").Get<JwtConfig>();
            Byte[] seckeyBytes = Encoding.UTF8.GetBytes(config.Key);

            var tokenValidPara = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(seckeyBytes),
                ClockSkew=TimeSpan.Zero,
                 ValidateLifetime=false

            };
            ApiResponse apiRes = new ApiResponse();
            try
            {
               
                var tokenValidation = tokenSecurityTokenHandler.ValidateToken(model.Jwt,tokenValidPara
                    ,out var validatedToken);
                if (validatedToken != null && validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                       apiRes=  new ApiResponse()
                        {
                            Success = false,
                            Message = "InvalidToken"
                        };
                    }
                }
                
            }
            catch
            {

            }
            finally
            {

            }
            return Ok(apiRes);

        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
              
            }
            return Convert.ToBase64String(random);
        }

        /// <summary>
        /// kiem tra login info vs DB
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns>LoginInfo: ket qua login</returns>
        private LoginInfo? AuthenticateUser(UserModel login)
        {
            LoginInfo user = null;

            //Validate the User Credentials
            //Demo Purpose, I have Passed HardCoded User Information
            if (login.Username == "Bac")
            {
                user = new LoginInfo { UserID=Guid.NewGuid(), FullName = "Bac", EmailAddress = "lamhong.bac@gmail.com", Roles="admin" };
            }
            return user;
        }
    }
}
