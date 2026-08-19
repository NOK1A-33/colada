using System.Security.Claims;
using Messanger.Api.Services;
using Messanger.Api.Services.Interfaces;
using Messenger.Repository.Interfaces;
using Messenger.Repository.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "auth-cookie";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });
 

var app = builder.Build(); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
