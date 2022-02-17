using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingAPI.Configurations
{
    public class TestOptionMiddleWare : IMiddleware
    {
        TestOption testOption;
        public TestOptionMiddleWare(IOptions<TestOption> _testOption)
        {
            testOption = _testOption.Value;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            ImageConfiguration imageConfig = testOption.ImageConfig;

            StringBuilder st = new StringBuilder();

            st.Append("Test option middleware \n");
            st.Append($"Company Type {testOption.CompanyType}\n");
            st.Append($"ConnectionString {testOption.CompanyType}\n");
            st.Append($"ImageDefaultPath  {imageConfig.DefaultPath}\n");
            st.Append($"ImageType {imageConfig.DefaultType}\n");
            st.Append($"ImageSize {imageConfig.DefaultSize}\n");
            await context.Response.WriteAsync(st.ToString());
            await next(context);

        }
    }
}
