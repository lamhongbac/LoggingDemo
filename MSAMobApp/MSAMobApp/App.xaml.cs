using MSAMobApp.Services;
using MSAMobApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSAMobApp
{
    public partial class App : Application
    {
       public static XAppContext AppContext;
        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            AppContext = XAppContext.GetInstance();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
