using Microsoft.AspNetCore.Authorization;

namespace JWTAPI.Auth
{
    /// <summary>
    /// parametter cho autho
    /// object-right
    /// profile-update
    /// </summary>
    public class RoleRequirement:IAuthorizationRequirement
    {
        public RoleRequirement(string name)
        {
            ObjectRight = name;
        }
        public string ObjectRight { get; set; }
    }
    /// <summary>
    /// gia dinh day la lop thuc hien quan tri vai tro va object
    /// 1 user login se co 1 role va danh sach object va quyen a-read, b-Update,c-Deleted
    /// </summary>
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type.ToLower() == "objectrights"))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            string objectrights = context.User.Claims.FirstOrDefault(x => x.Type.ToLower() == "objectrights").Value;
            List<string> object_rights = objectrights.Split(';').ToList();

            if (object_rights.Any(x=>x==requirement.ObjectRight))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }



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
