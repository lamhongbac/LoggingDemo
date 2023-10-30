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
            //ID cua access token
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
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
        public async Task<IActionResult> ReNewToken(JwtData model)
        {
            JwtSecurityTokenHandler tokenSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtConfig config= _config.GetSection("Jwt").Get<JwtConfig>();
            Byte[] seckeyBytes = Encoding.UTF8.GetBytes(config.Key);

            //b1. build token validate para
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
               //b2 check token is Valid
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
                        return Ok(apiRes);
                    }
                }
                //check expired
                var utcExpired = long.Parse(tokenValidation.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixTimeToDate(utcExpired);
                if (expireDate > DateTime.UtcNow)
                {
                    apiRes = new ApiResponse()
                    {
                        Success = false,
                        Message = "TokenIsNotExpired"
                    };
                    return Ok(apiRes);

                }
                //check reftoken is existed
                var storedToken = _tokenDatas.RefreshTokens.FirstOrDefault(x => x.Token == model.RefreshToken);
                if (storedToken == null)
                {
                    apiRes = new ApiResponse()
                    {
                        Success = false,
                        Message = "TokenIsNotExist"
                    };
                    return Ok(apiRes);
                }
                //check token is used/revoked
                if (storedToken.IsUsed)
                {
                    apiRes = new ApiResponse()
                    {
                        Success = false,
                        Message = "TokenIsUsed"
                    };
                    return Ok(apiRes);
                }
                if (storedToken.IsRevoked)
                {
                    apiRes = new ApiResponse()
                    {
                        Success = false,
                        Message = "TokenIsRevoked"
                    };
                    return Ok(apiRes);
                }
                //Check access token ID is correct
                var jti = tokenValidation.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value;

                if (storedToken.JwtId!=jti)
                {
                    apiRes = new ApiResponse()
                    {
                        Success = false,
                        Message = "AccessTokenIdIsNotMatch"
                    };
                    return Ok(apiRes);
                }

                //Update token
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _tokenDatas.Update(storedToken);
                LoginInfo userInfo = new LoginInfo();

                //su dung old token de lay lai cac thong tin cu
                // username

                var token = GenerateJSONWebToken(userInfo);


            }
            catch
            {

            }
            finally
            {

            }
            return Ok(apiRes);

        }

        private DateTime ConvertUnixTimeToDate(long utcExpired)
        {
            var dateTimeInterval=new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpired).ToUniversalTime();
            return dateTimeInterval;
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
