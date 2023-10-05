using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

//1 add PATH of resource for localization
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
//2
builder.Services.AddMvc().AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Add services to the container.
builder.Services.AddControllersWithViews();

const string culture = "vi-VN";
//2 Add CultureInfo
builder.Services.Configure<RequestLocalizationOptions>(options => {
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),      
        new CultureInfo("vi-VN")
    };
    options.DefaultRequestCulture = new RequestCulture(culture: culture, uiCulture: culture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    //
    //for custom code 
    //
    //options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
    //{
    //    //Write your code here:store DB
    //    return new ProviderCultureResult("en");
    //}));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
