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
builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
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
            
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RolePolicy", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();


        // add the custom requirement to the policy
        policy.Requirements.Add(new RoleRequirement("any"));
    });
});


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
