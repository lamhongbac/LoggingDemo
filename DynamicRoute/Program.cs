using DynamicRoute.Helper.Alias;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

builder.Services.AddSingleton<TranslationTransformer>();
builder.Services.AddSingleton<TranslationDatabase>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseExceptionHandler("/Error");
//app.UseStatusCodePagesWithRedirects("/Error/{0}");
//app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDynamicControllerRoute<TranslationTransformer>
    ("{language_alias?}/{page_alias}/{para_alias?}");
});

//endpoints.MapDynamicControllerRoute<PageTransformer>("pages/{**slug}");
app.MapDefaultControllerRoute();


app.MapControllerRoute(
    name: "listing",
    pattern: "{controller}/{action=Index}/{lang=vn}/{para?}");

//app.MapControllerRoute(
//    name: "detail",
//    pattern: "{controller=Home}/{action=detail}/{lang}/{?number}");
app.UseAuthorization();

app.Run();
