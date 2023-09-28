using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCWeb.Helper.Alias
{
    public class TranslationDatabase
    {
        private static Dictionary<string, Dictionary<string, string>> Translations = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "en", new Dictionary<string, string>
            {
                { "alias", "alias" },
                { "news", "news" }
            }
        },
        {
            "vn", new Dictionary<string, string>
            {
                { "dinh-danh", "dinh-danh" },
                { "tin-tuc", "tin-tuc" }
            }
        },
        
    };

        public async Task<string> Resolve(string lang, string value)
        {
            var normalizedLang = lang.ToLowerInvariant();
            var normalizedValue = value.ToLowerInvariant();
            if (Translations.ContainsKey(normalizedLang) && Translations[normalizedLang].ContainsKey(normalizedValue))
            {
                return Translations[normalizedLang][normalizedValue];
            }

            return null;
        }
    }
}
