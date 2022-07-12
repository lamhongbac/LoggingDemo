using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFMaterialDesignStudy.Lib;

namespace WPFMaterialDesignStudy.ViewModel
{
  public  class LoginViewModel:ViewModelBase
    {
        public string  UserID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool isLogined { get; private set; }
        public ICommand LoginCommand { get; set; }
        
        public LoginViewModel()
        {
            FullName = "Demo User";
            isLogined = true;

            LoginCommand = new RelayCommand<Window>((p) => { return true; },
                (p)=>{ Login(p); });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void Login(Window p)
        {
            string passcode = MD5Hash(EncodeBase64(Password);
            if (p == null)
            {
                return;
            }
            isLogined = true;
            p.Close();
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

            for (int i=0;i<bytes.Length;i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    }
}
