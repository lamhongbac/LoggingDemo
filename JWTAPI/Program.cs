using JWTAPI.Auth;
using JWTAPI.Models;
using JWTAPI.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient< IConfigureOptions < SwaggerGenOptions > ,SwaggerConfigOption >();
//RefreshTokenDatas
builder.Services.AddSingleton<RefreshTokenDatas>();

JwtConfig jwtconfiguration = builder.Configuration.GetSection("Jwt").Get<JwtConfig>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //tu cap token?
            ValidateIssuer = true,
            ValidateAudience = true,

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtconfiguration.Issuer,
            ValidAudience = jwtconfiguration.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtconfiguration.Key)),
            ClockSkew=TimeSpan.Zero
        };
    });

// add 1 danh sach cac policy vao Services

builder.Services.AddAuthorization(options =>
{
    //
    //policy ten la "profile-update", se dc su dung tren action voi policy name Policy(Name="profile-update")
    // no se nhan para ="profile-update", sau do can cu vao do de xu ly
    //RoleRequirementHandler : AuthorizationHandler<RoleRequirement>, Ham handler chi dinh xu ly vi no handler
    //type RoleRequirement
    //

    options.AddPolicy("profile-update", policy =>
    {
        policy.AddRequirements(new RoleRequirement("profile-update"));
    });
    options.AddPolicy("changepwd-update", policy =>
    {
        policy.AddRequirements(new RoleRequirement("changepwd-update"));
    });

});
builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

//builder.Services.AddAuthorization(x => {
//    x.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName));
//    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
