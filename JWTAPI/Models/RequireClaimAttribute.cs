using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JWTAPI.Models
{
    /// <summary>
    /// kiem tra dua tren cac gia tri cua claim
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireClaimAttribute : Attribute, IAuthorizationFilter
    {
        string _claimName;
        string _claimValue;
        public RequireClaimAttribute(string claimName,string claimValue)
        {
            _claimName = claimName;
            _claimValue = claimValue;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasClaim(_claimName, _claimValue))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
