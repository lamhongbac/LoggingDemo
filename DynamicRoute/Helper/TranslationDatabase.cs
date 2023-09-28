using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicRoute.Helper.Alias
{
    public class TranslationDatabase
    {
        //cover dic
        private static Dictionary<string, Dictionary<string, string>> ResolveDictionary = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "en", new Dictionary<string, string>
            {
                { "order", "order-index" },
                { "promotion", "news-index" },
                { "order-detail", "order-detail" },
                { "promotion-detail", "news-detail" },
            }
        },
        {
           "vn", new Dictionary<string, string>
            {
                { "don-hang", "order-index" },
                { "khuyen-mai", "news-index" },
                { "chi-tiet-don-hang", "order-detail" },
                { "chi-tiet-khuyen-mai", "news-detail" },
            }
        },
        
    };
       
    
        public async Task<string> ResolveCover(string lang, string value)
        {

         
             var normalizedLang = lang.ToLowerInvariant();
            var normalizedValue = value.ToLowerInvariant();
           
            if (ResolveDictionary.ContainsKey(normalizedLang) && ResolveDictionary[normalizedLang].ContainsKey(normalizedValue))
            {
                return ResolveDictionary[normalizedLang][normalizedValue];
            }

            return null;
        }
    }
}
