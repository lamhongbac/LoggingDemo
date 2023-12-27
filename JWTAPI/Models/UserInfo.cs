using System.Data;
using System.Net.Mail;

namespace JWTAPI.Models
{
    public class UserInfo
    {

        public UserInfo()
        {

        }

        public UserInfo(string id, string userName)
        {
            ID = id;
            UserName = userName;
            FullName = userName;
            EmailAddress = "lamhong.bac@gmail.com";
            Roles = "admin";
            ObjectRights = "profile-update;changepwd-read";
        }


        public UserInfo(string id, string userName, string fullName, 
            string emailAddress, string roles)
        {

            ID=id;
            UserName=userName;
            FullName=fullName;
            EmailAddress=emailAddress;
            Roles = roles;
            ObjectRights = "profile-update;changepwd-read";
            //CustomsClaims = new Dictionary<string, object>();
        }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Roles { get; set; }
        //public Dictionary<string, object> CustomsClaims { get; set; }
        public string ObjectRights { get; set; } //object1-right;object2-right

    }
}
