using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MSAMobApp.Droid;
using MSAMobApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDevice))]
namespace MSAMobApp.Droid
{
    public class AndroidDevice : IDevice
    {
        public string GetIdentifier()
        {
            return Android.Provider.Settings.Secure.GetString
                (Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                }
    }
}