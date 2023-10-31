using System.Data;

namespace JWTAPI.Models
{
    public class UserInfo
    {
        public UserInfo()
        {

        }
        public UserInfo(string id, string userName, string fullName, 
            string emailAddress, string roles)
        {

            ID=id;
            UserName=userName;
            FullName=fullName;
            EmailAddress=emailAddress;
            Roles = roles;
            //CustomsClaims = new Dictionary<string, object>();
        }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Roles { get; set; }
        //public Dictionary<string, object> CustomsClaims { get; set; }
    }
}
