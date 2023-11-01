namespace JWTClient.Models
{
    public class UserProfileModel
    {
        public UserProfileModel()
        {


            CustomsClaims = new Dictionary<string, object>();
        }
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }

        public string Roles { get; set; }
        public string Password { get; set; }
        public Dictionary<string, object> CustomsClaims { get; set; }
    }
}
