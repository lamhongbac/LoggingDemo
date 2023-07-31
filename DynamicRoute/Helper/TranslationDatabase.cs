using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicRoute.Helper.Alias
{
    public class TranslationDatabase
    {
        //cover dic
        private static Dictionary<string, Dictionary<string, string>> ControllerDictionary = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "en", new Dictionary<string, string>
            {
                { "order", "order" },
                { "promotion", "news" },
               
            }
        },
        {
            "vn", new Dictionary<string, string>
            {
                { "don-hang", "order" },
                { "khuyen-mai", "news" },
              
            }
        },
        
    };
        private static Dictionary<string, Dictionary<string, string>> ActionDictionary = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "en", new Dictionary<string, string>
            {
                { "detail", "detail" },
                { "index", "index" },
               
            }
        },
        {
            "vn", new Dictionary<string, string>
            {
                { "chi-tiet", "detail" },
                { "danh-sach", "index" },
               
            }
        },

    };
        public async Task<string> ResolveCover(string dic,string lang, string value)
        {
            Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();

            var normalizeddic = dic.ToLowerInvariant();
             var normalizedLang = lang.ToLowerInvariant();
            var normalizedValue = value.ToLowerInvariant();
            if (normalizeddic=="controller" )
            {
                dictionary = ControllerDictionary;

            }
            else
            {
                dictionary = ActionDictionary;
            }
            if (dictionary.ContainsKey(normalizedLang) && dictionary[normalizedLang].ContainsKey(normalizedValue))
            {
                return dictionary[normalizedLang][normalizedValue];
            }

            return null;
        }
    }
}
