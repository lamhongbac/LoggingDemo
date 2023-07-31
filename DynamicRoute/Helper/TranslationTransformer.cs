using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
            

            //values.Clear();
            //values["controller"] = "order";
            //values["action"] = "detail";
            //values["para"] = "?lang=vn&number=123";
            //values["lang"] = "vn";
            //values["number"] = "123";
            //return values;

            int iCount = values.Count();
            bool isCover = (iCount == 2);

           

            string language_alias = "";
            string language = "vn";
            string page_alias = "";
            string controller = "";
            string action = "";
           

            string para_alias = "";
            string para = "";

             language_alias = (string)values["language_alias"];
             page_alias = (string)values["page_alias"];
             para_alias = (string)values["para_alias"];
            values.Clear();

            if (language_alias==null)
            {
               language = "vn";
            }
            else
            {
                language = language_alias;
            }
            values["lang"] = language;

            if (page_alias != null)
            {


                string controller_action = await _translationDatabase.ResolveCover(language, page_alias);
                if (controller_action != null)
                {
                    string[] arrs = controller_action.Split("-");
                    controller = arrs[0];
                    action = arrs[1];

                    values["controller"] = controller;
                    values["action"] = action;
                }

            }



            if (para_alias!=null)
            {
               
                
                para = para_alias;
                //chua tinh den -data_alias-number
               

                List<string> paras = para.Split("&").ToList();
                //Index": string lang, int pageIndex = -1, string filter = ""
                foreach (var item in paras)
                {
                    string[] Item_para = item.Split("=");
                    if (Item_para.Length == 1)
                    {
                        if (Item_para[0] == "number")
                        {
                            //tach data-alias
                            values[Item_para[0]] =      Item_para[1].Split("-")[1];
                        }
                        else
                        {
                            values[Item_para[0]] = Item_para[1];
                        }
                    }

                }



                
            }


           

            values["controller"] = controller;
            values["action"] = action;

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
