using DEMOService.Configuration;
using System;

namespace DEMOService
{
    public class DemoService
    {
        private string comapnyCode;
        public string CompanyCode
        {
            get
            {

                return comapnyCode;

            }

            set { comapnyCode = value; }
        }
       private  AppConfiguration appConfiguration;
      private static  DemoService instance = null;

        public static void Configure(AppConfiguration appConfiguration)
        { 
            if (instance==null)
            {
                instance = new DemoService();
                
            }
            instance.SetConfiguration(appConfiguration);
            //return instance;
        }

        private void SetConfiguration(AppConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
            comapnyCode = appConfiguration.CompanyCode;
        }

        public static DemoService GetInstance()
        {
            if ( instance == null)
            {
                instance = new DemoService();
            }
            return instance;
        }
        DemoService()
        {
           
            //comapnyCode = appConfiguration.CompanyCode;
        }

    }
}
