using DAL;
using LibraryLogging;
using LoggingAPI.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoggingAPI", Version = "v1" });
            });
            services.AddOptions();
            services.Configure<TestOption>(Configuration.GetSection("TestOption"));
            //sau hanh dong nay thi IOption<TestOption> se co value= TestOption 
            //IOption<TestOption> da dc dang ky vao trong service
            // va vi the trong cac class co para 
            // va trong tap hop cac services (DI) da co IOption<TestOption>

            services.AddTransient<TestOptionMiddleWare>();


            //var confifg = 
            //    Configuration.GetSection("ApplicationConfiguration").GetChildren();
            CompanyProcessConfiguration configuration = Configuration.GetSection("ApplicationConfiguration").Get<CompanyProcessConfiguration>();
            //CompanyProcessConfiguration configuration = (CompanyProcessConfiguration)
            //    Configuration.GetSection("ApplicationConfiguration").GetChildren();

            string connectionString = configuration.ConnectionString;
            //CompanyProcessConfiguration configuration = (CompanyProcessConfiguration)
            //    Configuration.GetSection("ApplicationConfiguration").GetChildren();

            services.AddSingleton<IConnnectionStringManager>(x =>
            {
                return new ConnnectionStringManager(connectionString);
            });

            services.AddSingleton(typeof(IMyDAL<>), typeof(MyDAL<>));

            //ILogger<CompanyBusinessProcess> logger=services.get
            services.AddScoped<ICompanyBusinessProcess, CompanyBusinessProcess>(x =>
             {
                 var logger = x.GetRequiredService<ILogger<CompanyBusinessProcess>>();
                 var dal = x.GetRequiredService<IMyDAL<CompanyData>>();
                 return new CompanyBusinessProcess(logger, dal, configuration);
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoggingAPI v1"));
            }
            //app.UseMiddleware<TestOptionMiddleWare>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                 {
                     await context.Response.WriteAsync("Welcome study");
                 });
                endpoints.MapGet("/ShowOptions1", async context =>
                {
                    //bien config tuong duong gia tri bien Configuration
                    var config = context.RequestServices.GetService<IConfiguration>();
                    var sectionTestOption = config.GetSection("TestOption");
                    var companytype = sectionTestOption["CompanyType"];
                    var connectionString = sectionTestOption["ConnectionString"];
                    //for group of configuration using GetSection
                    var imageConfiguration = sectionTestOption.GetSection("ImageConfiguration"); ;
                    var imageDefaultPath = imageConfiguration["DefaultPath"];
                    var imageType = imageConfiguration["DefaultType"];
                    var imageSize = imageConfiguration["DefaultSize"];

                    StringBuilder st = new StringBuilder();

                    st.Append("Test option1 \n");
                    st.Append($"Company Type {companytype}\n");
                    st.Append($"ConnectionString {connectionString}\n");
                    st.Append($"ImageDefaultPath  {imageDefaultPath}\n");
                    st.Append($"ImageType {imageType}\n");
                    st.Append($"ImageSize {imageSize}\n");

                    await context.Response.WriteAsync(st.ToString());
                });

                // su dung lop TestOption
                endpoints.MapGet("/ShowOptions", async context =>
                {
                    TestOption testOption = context.RequestServices.
                    GetService<IOptions<TestOption>>().Value;
                    ImageConfiguration imageConfig = testOption.ImageConfig;

                    StringBuilder st = new StringBuilder();

                    st.Append("Test option2 \n");
                    st.Append($"Company Type {testOption.CompanyType}\n");
                    st.Append($"ConnectionString {testOption.CompanyType}\n");
                    st.Append($"ImageDefaultPath  {imageConfig.DefaultPath}\n");
                    st.Append($"ImageType {imageConfig.DefaultType}\n");
                    st.Append($"ImageSize {imageConfig.DefaultSize}\n");
                    await context.Response.WriteAsync(st.ToString());

                });
                loggerFactory.AddFile("Logs/mylog-{Date}.txt");

            });
        }
    }
}
