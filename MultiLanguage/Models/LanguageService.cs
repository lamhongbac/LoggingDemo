using Microsoft.Extensions.Localization;
using MultiLanguage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MultiLanguage.Models
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("ShareResource", assemblyName.Name);
        }

        public LocalizedString Getkey(string key)
        {
            var local= _localizer[key];
            return local;
        }
    }
}
