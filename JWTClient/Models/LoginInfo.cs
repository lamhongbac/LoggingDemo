﻿namespace JWTClient.Models
{
    public class UserInfo
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Roles { get; set; }
    }
    public class LoginInfo
    {
        public LoginInfo()
        {
            LoginDate = DateTime.Now;
            JwtData = new JwtData();

        }

        public DateTime LoginDate { get; set; }
        //example cho truong hop user data tong quat duoi dang string object

        public JwtData JwtData { get; set; }
    }
}
