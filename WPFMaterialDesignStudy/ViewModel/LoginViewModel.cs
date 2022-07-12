using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFMaterialDesignStudy.Lib;

namespace WPFMaterialDesignStudy.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public UserModel LoginUser { get; set; }
        string userID;
        public string UserID
        {
            get => userID; set => SetProperty(ref userID, value); //{ get => "FullName: " + memberProfile.FullName; }
        }
       
        string password;
        public string Password
        {
            get => password; set => SetProperty(ref password, value); //{ get => "FullName: " + memberProfile.FullName; }
        }
        string fullName;
        public string FullName
        {
            get => fullName; set => SetProperty(ref fullName, value); //{ get => "FullName: " + memberProfile.FullName; }
        }
        public bool IsLogined { get; private set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public LoginViewModel()
        {
            

               FullName = "Demo User";
            IsLogined = false;

            LoginCommand = new RelayCommand<Window>((p) => { return true; },
                (p) => { Login(p); });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void Login(Window p)
        {
            //string passcode = MD5Hash(EncodeBase64(Password));
            //if (p == null)
            //{
            //    return;
            //}
            /////login success
            //List<string> roles = new List<string>() { };
            //LoginUser = new UserModel(UserID,password, roles);
            //isLogined = passcode.Length>0;
          
            IsLogined = true; p.Close();
        }

        public static string EncodeBase64(string plaintext)
        {
            var plaintextbyte = System.Text.Encoding.UTF8.GetBytes(plaintext);
            return System.Convert.ToBase64String(plaintextbyte);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Sp = new MD5CryptoServiceProvider();
            byte[] bytes = md5Sp.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    }
}
