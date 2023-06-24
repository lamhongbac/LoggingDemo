namespace WebAPI
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private
        const string APIKEY = "XApiKey";
        const string CompanyCode = "CompanyCode";
        const string BranchCode = "BranchCode";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEY, out
                    var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided ");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEY);
            string companyCode = appSettings.GetValue<string>(CompanyCode);
            string branchCode = appSettings.GetValue<string>(BranchCode);

            if (!IsValidKey(extractedApiKey, companyCode, branchCode))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);
        }
    
    public bool IsValidKey(string key,string companyCode, string branchCode)
        {
            bool OK = false;
            try
            {
                string decode = Base64Tool.Base64Decode(key);
                List<string> keys = decode.Split(":").ToList();

                OK = keys.Count == 2
                   && keys[0] == companyCode
                   && keys[1] == branchCode;
            }catch(Exception ex) 
            { 
                string message = ex.Message;
                return OK;
            }
            return OK;
        }
    }

    public class Base64Tool
    {
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
