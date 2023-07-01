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
        AppConfiguration appConfiguration;
      private static  DemoService instance = null;

        public static DemoService GetInstance(AppConfiguration appConfiguration, bool isReload=false)
        { 
            if (isReload || instance==null)
            {
                instance = new DemoService(appConfiguration);
            }
            return instance;
        }
        DemoService(AppConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
            comapnyCode = appConfiguration.CompanyCode;
        }

    }
}
