using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using static MVCWeb.Models.AppSettingManagement;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DEMOService.Configuration;

namespace MVCWeb.Models
{
    public class AppSettingHelper
    {
        public static string parentGroup = "AppConfig";
        IConfiguration configuration;
     
        IWritableOptions<AppConfiguration> _writableAppConfig;
        
        public AppSettingHelper(IConfiguration config,          
            IWritableOptions<AppConfiguration> writableAppConfig)
        {
            this.configuration = config;       
            _writableAppConfig = writableAppConfig;
           
        }
        /// <summary>
        /// doc va dua ra view de sua
        /// public enum EP2AppSettingGroup
        ///
        ///AppConfig, CMSNewsConfiguration, DBConfiguration, SMSConfig,
        ///LoyaltyServiceConfiguration, OTPConfig
        ///
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        //public IDictionary<string, object> ReadGroupSetting(string group)
        //{
        //    IDictionary<string, object> result = new Dictionary<string, object>();
        //    var dbConfigSec = configuration.GetSection("DBConfiguration");
        //    var sectionConfig = configuration.GetSection(group);

        //    //DBConfiguration
        //    if (group.ToLower() == EP2AppSettingGroup.DBConfiguration.ToString().ToLower())
        //    {
        //        DBConfig appConfiguration = sectionConfig.Get<DBConfig>();

        //        result = appConfiguration.ToDictionary();
        //        return result;
        //    }
        //    //AppConfig
        //    if (group.ToLower() == EP2AppSettingGroup.AppConfig.ToString().ToLower())
        //    {
        //        CRMManagerAppConfig appConfiguration = sectionConfig.Get<CRMManagerAppConfig>();
        //        result = appConfiguration.ToDictionary(); return result;
        //    }
        //    //CMSNewsConfiguration
        //    if (group.ToLower() == EP2AppSettingGroup.CMSNewsConfiguration.ToString().ToLower())
        //    {
        //        CMSNewsConfiguration appConfiguration = sectionConfig.Get<CMSNewsConfiguration>();
        //        result = appConfiguration.ToDictionary(); return result;
        //    }
        //    //SMS
        //    if (group.ToLower() == EP2AppSettingGroup.SMSConfig.ToString().ToLower())
        //    {
        //        SMSConfig appConfiguration = sectionConfig.Get<SMSConfig>();
        //        result = appConfiguration.ToDictionary(); return result;
        //    }
        //    //OTP
        //    //if (group.ToLower() == EP2AppSettingGroup.OTPConfig.ToString().ToLower())
        //    //{
        //    //    OTPConfig appConfiguration = sectionConfig.Get<OTPConfig>();
        //    //    result = appConfiguration.ToDictionary(); return result;
        //    //}
        //    //LoyaltyServiceConfiguration
        //    if (group.ToLower() == EP2AppSettingGroup.LoyaltyServiceConfiguration.ToString().ToLower())
        //    {
        //        LoyaltyServiceConfig appConfiguration = sectionConfig.Get<LoyaltyServiceConfig>();
        //        result = appConfiguration.ToDictionary(); return result;
        //    }
        //    return result;

        //}

        //public static Dictionary<string, object> ToDictionary(object obj)
        //{
        //    var json = JsonConvert.SerializeObject(obj);
        //    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        //    return dictionary;
        //}
        /// <summary>
        /// view sua xong nho ban nay ghi xuong
        /// </summary>
        /// <param name="group"></param>
        /// <param name="key"></param>
        /// <param name="stringValue"></param>
        public bool WriteSetting
            ( string key, string stringValue)
        {
            bool result = false;
            try
            {
                //if (appCode=)
                var appSettingsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
                var json = File.ReadAllText(appSettingsPath);
                var jsonSettings = new JsonSerializerSettings();
                dynamic jsonObj = JsonConvert.DeserializeObject(json);




                if (key.ToLower() == "companycode")
                {
                    _writableAppConfig.Update(opt =>
                    {
                        opt.CompanyCode = stringValue;
                    });
                }
                if (key.ToLower() == "message")
                {
                    _writableAppConfig.Update(opt =>
                    {
                        opt.Message = stringValue;
                    });
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        
    }
    public class AppSettingManagement
    {
        public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
        {
            void Update(Action<T> applyChanges);
        }
        public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
        {
            private readonly IWebHostEnvironment _environment;
            private readonly IOptionsMonitor<T> _options;
            private readonly IConfigurationRoot _configuration;
            private readonly string _section;
            private readonly string _file;

            public WritableOptions(
                IWebHostEnvironment environment,
                IOptionsMonitor<T> options,
                IConfigurationRoot configuration,
                string section,
                string file)
            {
                _environment = environment;
                _options = options;
                _configuration = configuration;
                _section = section;
                _file = file;
            }

            public T Value => _options.CurrentValue;
            public T Get(string name) => _options.Get(name);

            public void Update(Action<T> applyChanges)
            {
                var fileProvider = _environment.ContentRootFileProvider;
                var fileInfo = fileProvider.GetFileInfo(_file);
                var physicalPath = fileInfo.PhysicalPath;

                var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
                var sectionObject = jObject.TryGetValue(_section, out JToken section) ?
                    JsonConvert.DeserializeObject<T>(section.ToString()) : (Value ?? new T());

                applyChanges(sectionObject);

                jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
                File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
                _configuration.Reload();
            }
        }
       
    }
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureWritable<T>(
            this IServiceCollection services,
            IConfigurationSection section,
            string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableOptions<T>>(provider =>
            {
                var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
                var environment = provider.GetService<IWebHostEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new WritableOptions<T>(environment, options, configuration, section.Key, file);
            });
        }
    }
    public static class ObjectToDictionaryHelper
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static void SetPropValue(this object ship, string propName, string value)
        {


            PropertyInfo propertyInfo = ship.GetType().GetProperty(propName);
            propertyInfo.SetValue(ship, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }
        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}
