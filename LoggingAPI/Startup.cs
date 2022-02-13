using DAL;
using LibraryLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoggingAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");

        }
    }
}
