using System;
using System.Collections.Generic;
using System.Text;

namespace WPFMaterialDesignStudy.Lib
{
   public class UserModel
    {
        public UserModel(string userID,string fullName,List<string> userRoles)
        {
            UserID = userID; 
            FullName = fullName;
            Roles = UserRoleModel.GetRoles(userRoles);
            //Roles = new List<UserRoleModel>()
            //{
            //    new UserRoleModel() { Objects= new List<string>() {"object1", "object2", "object3" }, RoleID="User", RoleName="user role" },
            //    new UserRoleModel() { Objects= new List<string>() {"object1", "object2", "object3","object4","object5" }, RoleID="Admin", RoleName="adin role" }
            //};
        }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public List<UserRoleModel> Roles { get; set; }
    }

    public class UserRoleModel
    {
        private string item;

        public UserRoleModel(string item)
        {
            this.item = item;
        }

        public string RoleID { get; set; }
        public string RoleName { get; set; }
        /// <summary>
        /// object rold dc phep
        /// </summary>
        public List<string> Objects { get; set; }

        public static List<UserRoleModel> GetRoles(List<string> userRoles)
        {
            List<UserRoleModel> roles = new List<UserRoleModel>();
            foreach (var item in userRoles)
            {
                UserRoleModel role = new UserRoleModel(item);
                roles.Add(role);
            }
            return roles;
        }
    }
}
