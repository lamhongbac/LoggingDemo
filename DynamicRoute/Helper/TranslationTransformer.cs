using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace DynamicRoute.Helper.Alias
{
    public class TranslationTransformer : DynamicRouteValueTransformer
    {
        private readonly TranslationDatabase _translationDatabase;

        public TranslationTransformer(TranslationDatabase translationDatabase)
        {
            _translationDatabase = translationDatabase;
        }

        public override async ValueTask<RouteValueDictionary> TransformAsync
            (HttpContext httpContext, RouteValueDictionary values)

        { 
            int iCount=values.Count();
            bool isCover = (iCount == 2);
           
            bool isContainLan = values.ContainsKey("language");
            bool isContainPage = values.ContainsKey("page_alias");

            if (!values.ContainsKey("language") || !values.ContainsKey("page_alias") ) return values;
           
            string controller = "";
            var language = (string)values["language"];
           //if (language != null && language!="vn" || language!="en" )
           // {
           //     language = "vn";

           // }
            //cover action ="index"
            string dic = "cover";
            string action = "index";
            string para = "";
            if (!isCover)
            {
                dic = "detail";
                action = "detail";
                para = values["para"].ToString();
            }
            //xac dinh controller thong qua page alias
            controller = await _translationDatabase.ResolveCover(dic, language, (string)values["page_alias"]);

            if (controller == null) return values;

            values["controller"] = controller;

            string[] paras = para.Split("-").ToArray();
            string number = "";
            string data_alias = "";
            if (paras.Length ==2 ) 
            { 
                number = paras[1];
                data_alias = paras[0];
            }

            if (action == null) return values;
            values["action"] = action;
            if (!isCover && !string.IsNullOrWhiteSpace(number))
            {
                values["number"] = number;
            }
            return values;
        }
    }
}
