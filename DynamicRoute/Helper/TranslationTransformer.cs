using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


            string language = "vn";
            string controller_alias = "";
            string controller = ""; 
            string action = "";
            string action_alias = "";
           
            string para_alias = "";
            string para = "";
            if (!values.ContainsKey("language"))
            {
                values["language"] = "vn";
            }
            else
            {
                language = (string)values["language"];
            }

            if (values.ContainsKey("control_alias"))
            {
                controller_alias = (string)values["control_alias"];
                controller = await _translationDatabase.ResolveCover("controller",language, (string)values["control_alias"]);
                values.Remove("control_alias");
                values["controller"] = controller;
            }
           
            if (values.ContainsKey("action_alias"))
            {
                action_alias = (string)values["action_alias"];
                action = await _translationDatabase.ResolveCover("action", language, (string)values["action_alias"]);
                values.Remove("action_alias");
                values["action"] = action;
            }
           
            if (values.ContainsKey("para_alias"))
            {
                para_alias = (string)values["para_alias"];
                para = para_alias;
                //chua tinh den -data_alias-number
                values["para"] = para;
                // if action=Index
                //XAC DINH LAI cac route para [code, number,...vv]
            }
            

            //cover action ="index"

           

            if (controller == null) return values;

            

            //string[] paras = para.Split("-").ToArray();
            //string number = "";
            //string data_alias = "";
            //if (paras.Length ==2 ) 
            //{ 
            //    number = paras[1];
            //    data_alias = paras[0];
            //}

            //if (action == null) return values;
            //values["action"] = action;
            //if (!isCover && !string.IsNullOrWhiteSpace(number))
            //{
            //    values["number"] = number;
            //}
            return values;
        }
    }
}
