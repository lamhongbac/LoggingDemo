using JWTAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAPI.Controllers
{

    /// <summary>
    /// su dung de thuc hien cac thao tac tren lop user nhu
    /// GetProfile
    /// UpdateProfile
    /// ChangePassword
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateProfile(UserProfileModel model)
        {
            return Ok(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ChangePassword(ChangePwdModel model)
        {
            return Ok(model);
        }
    }
}
