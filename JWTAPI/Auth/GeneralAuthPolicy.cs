using Microsoft.AspNetCore.Authorization;

namespace JWTAPI.Auth
{
    /// <summary>
    /// parametter cho autho
    /// </summary>
    public class RoleRequirement:IAuthorizationRequirement
    {
        public RoleRequirement(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context, RoleRequirement requirement)
        {

             context.Succeed(requirement);
            return Task.CompletedTask;

            //if (!context.User.HasClaim(x => x.Type == "role"))
            //{
            //    return Task.CompletedTask;
            //}

            //var roleName = context.User.FindFirst(x => x.Type == "role").Value;
            //if (roleName == "admin")
            //{
            //    context.Succeed(requirement);
            //}
            //return Task.CompletedTask;
        }
    }
}
