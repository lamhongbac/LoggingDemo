using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SCMDAL.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSolution.Models;

namespace WHMSolution
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
            AppConfig appConfiguration = appConfigSection.Get<AppConfig>();
            string connectionString = appConfiguration.DBConfiguration.GetConnectionString();
            services.AddAutoMapper(typeof(AppAutoMapper));
            services.AddRazorPages();
            #region Add repo services
            //1. SettingRepository
            services.AddSingleton<MobStockMasterHandler>(x =>
            {
                //IMapper mapper = x.GetRequiredService<IMapper>();
                return new MobStockMasterHandler(appConfiguration.DBConfiguration.GetConnectionString());
            });
            services.AddSingleton<MobStockTransHandler>(x =>
            {
                //IMapper mapper = x.GetRequiredService<IMapper>();
                return new MobStockTransHandler(appConfiguration.DBConfiguration.GetConnectionString());
            });
            //AppConfig
            services.Configure<AppConfig>(options => Configuration.GetSection("AppConfig").Bind(options));

            
            services.AddSingleton<WHMApplication>();
            
            #endregion
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
