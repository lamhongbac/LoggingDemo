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
        public AppSettingViewModelHelper(AppSettingHelper appSettingHelper,
            IConfiguration configuration)
        {

            this.appSettingHelper = appSettingHelper;
            this.configuration = configuration;

        }
      public  List<AppSettingViewModel> GetItems()
        {
            List<AppSettingViewModel> list=new List<AppSettingViewModel>();
            IDictionary<string, object> result = new Dictionary<string, object>();
            var sectionConfig = configuration.GetSection("AppConfig");

            AppConfiguraiton appConfiguration = sectionConfig.Get<AppConfiguraiton>();
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
            return OK;
        }

        public AppSettingViewModel GetItem(string key)
        {
            AppSettingViewModel list = new AppSettingViewModel();
            IDictionary<string, object> result = new Dictionary<string, object>();
            var sectionConfig = configuration.GetSection("AppConfig");

            AppConfiguraiton appConfiguration = sectionConfig.Get<AppConfiguraiton>();
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