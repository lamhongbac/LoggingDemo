using DEMOService.Configuration;
using System;

namespace DEMOService
{
    public class DemoServiceSecond
    {
        private string comapnyCode;
        public string CompanyCode
        {
            get
            {
                if (appConfiguration != null)
                    comapnyCode = appConfiguration.CompanyCode;
                return comapnyCode;

            }

            set { comapnyCode = value; }
        }
       private  AppConfiguration appConfiguration;

        
        public   DemoServiceSecond(AppConfiguration appConfiguration)
        {

            this.appConfiguration = appConfiguration;
        }

        public void SetConfiguration(AppConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;

        }

       

    }
}
