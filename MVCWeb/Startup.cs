using DAL.Data;
using DEMOService;
using DEMOService.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCWeb.DataServiceFW;
using MVCWeb.Helper;
using MVCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb
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
            var appConfigSection = Configuration.GetSection("AppConfig");
            AppConfiguration appConfiguration = appConfigSection.Get<AppConfiguration>();
            //IWritableOptions<AppConfiguraiton> writableAppConfig
            services.ConfigureWritable<AppConfiguration>(appConfigSection);

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddAutoMapper(typeof(DataMapper));
            services.AddSingleton<StoreDataService>();
            services.AddSingleton<DataService>();
            services.AddSingleton<AppSettingHelper>();
            services.AddSingleton<AppSettingViewModelHelper>();
            //services.AddSingleton<AppConfiguraiton>(appConfiguration);
            //AppConfiguration appConfiguration = sectionConfig.Get<AppConfiguration>();
            DemoService.Configure(appConfiguration);
            DemoService demoService = DemoService.GetInstance();
            services.AddSingleton(appConfiguration);
            //demoService
            services.AddSingleton(demoService);
            services.AddSingleton<DemoServiceSecond>();
            services.AddSingleton<StoreDataHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
