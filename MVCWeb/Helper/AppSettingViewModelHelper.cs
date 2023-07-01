using DEMOService;
using DEMOService.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCWeb.Models;
using System;
using System.Collections.Generic;

namespace MVCWeb.Helper
{
    public class AppSettingViewModelHelper
    {
        IConfiguration configuration;
        AppSettingHelper appSettingHelper;
        DemoServiceSecond theSecond;
        public AppSettingViewModelHelper(AppSettingHelper appSettingHelper,
            IConfiguration configuration,DemoServiceSecond theSecond)
        {
            this.theSecond = theSecond;
            this.appSettingHelper = appSettingHelper;
            this.configuration = configuration;

        }
      public  List<AppSettingViewModel> GetItems()
        {
            List<AppSettingViewModel> list=new List<AppSettingViewModel>();
            IDictionary<string, object> result = new Dictionary<string, object>();
            var sectionConfig = configuration.GetSection("AppConfig");

            AppConfiguration appConfiguration = sectionConfig.Get<AppConfiguration>();
            result  = appConfiguration.ToDictionary();
            foreach (var item in result)
            {
                AppSettingViewModel mv = new AppSettingViewModel()
                {
                    Group = "AppConfig",
                    Key = item.Key,
                    Value = item.Value.ToString(),
                };

                list.Add(mv);
            }
            return list;
        }
        public bool Update(AppSettingViewModel viewModel)
        {
          bool OK=  appSettingHelper.WriteSetting(viewModel.Key, viewModel.Value);
            if (OK)
            {
                var sectionConfig = configuration.GetSection("AppConfig");

                AppConfiguration appConfiguration = sectionConfig.Get<AppConfiguration>();
                DemoService.Configure(appConfiguration);
                theSecond.SetConfiguration(appConfiguration);
            }
            return OK;
        }

        public AppSettingViewModel GetItem(string key)
        {
            AppSettingViewModel list = new AppSettingViewModel();
            IDictionary<string, object> result = new Dictionary<string, object>();
            var sectionConfig = configuration.GetSection("AppConfig");

            AppConfiguration appConfiguration = sectionConfig.Get<AppConfiguration>();
            result = appConfiguration.ToDictionary();
            foreach (var item in result)
            {
                if (item.Key == key)
                {
                    AppSettingViewModel mv = new AppSettingViewModel()
                    {
                        Group = "AppConfig",
                        Key = item.Key,
                        Value = item.Value.ToString(),
                    };
                    list = mv; break;
                }
                    
            }
            return list;
        }
    }
}