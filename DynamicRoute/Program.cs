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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    //endpoints.MapDynamicControllerRoute<TranslationTransformer>("{language}/{page_alias}/{id?}");
    endpoints.MapDynamicControllerRoute<TranslationTransformer>
    ("{language?}/{page_alias}/{para?}");
});

//endpoints.MapDynamicControllerRoute<PageTransformer>("pages/{**slug}");
app.MapDefaultControllerRoute();


app.MapControllerRoute(
    name: "listing",
    pattern: "{controller}/{action=Index}");

app.MapControllerRoute(
    name: "detail",
    pattern: "{controller=Home}/{action=detail}/{number?}");
app.UseAuthorization();

app.Run();
