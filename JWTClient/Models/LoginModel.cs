﻿namespace JWTClient.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }

        public bool KeepLogined { get; set; }
    }
}