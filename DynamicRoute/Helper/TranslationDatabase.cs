using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicRoute.Helper.Alias
{
    public class TranslationDatabase
    {
        //cover dic
        private static Dictionary<string, Dictionary<string, string>> CoverDictionary = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "en", new Dictionary<string, string>
            {
                { "order", "order" },
                { "promotion", "news" },
                { "store", "outlet" },
                { "event", "event" }
            }
        },
        {
            "vn", new Dictionary<string, string>
            {
                { "don-hang", "order" },
                { "khuyen-mai", "news" },
                { "cua-hang", "outlet" },
                { "su-kien", "event" }
            }
        },
        
    };
        private static Dictionary<string, Dictionary<string, string>> DetailDictionary = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "en", new Dictionary<string, string>
            {
                { "order-detail", "order" },
                { "promotion-detail", "news" },
                { "store-detail", "outlet" },
                { "event-detail", "event" }
            }
        },
        {
            "vn", new Dictionary<string, string>
            {
                { "don-hang-chi-tiet", "order" },
                { "khuyen-mai-chi-tiet", "news" },
                { "cua-hang-chi-tiet", "outlet" },
                { "su-kien-chi-tiet", "event" }
            }
        },

    };
        public async Task<string> ResolveCover(string dic,string lang, string value)
        {
            Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();

            var normalizeddic =dic.ToLowerInvariant();
            var normalizedLang = lang.ToLowerInvariant();
            var normalizedValue = value.ToLowerInvariant();
            if (normalizeddic=="cover" )
            {
                dictionary = CoverDictionary;

            }
            else
            {
                dictionary = DetailDictionary;
            }
            if (dictionary.ContainsKey(normalizedLang) && dictionary[normalizedLang].ContainsKey(normalizedValue))
            {
                return dictionary[normalizedLang][normalizedValue];
            }

            return null;
        }
    }
}
