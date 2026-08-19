using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Messanger.Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _UserRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EResultCode> LogValidation(string NameOrEmail, string Password)
        {
            var Users = await _UserRepository.GetAsync(); 
            var user = Users.Where(u => u.Name.Equals(NameOrEmail) || u.Email.Equals(NameOrEmail)).FirstOrDefault();
             
            if( user == null)
            {
                return EResultCode.NotFound;
            }
            var passwordHasher = new PasswordHasher<string>();

            var result = passwordHasher.VerifyHashedPassword("", user.Password, Password); 
            
            if(result == PasswordVerificationResult.Failed)
                return EResultCode.Invalid_Password;

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return EResultCode.DbError;

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            });
            if (result == PasswordVerificationResult.Success)
                return EResultCode.Success;
            else
                return EResultCode.Invalid_Password;
        }

        public async Task<EResultCode> LogOut()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return EResultCode.DbError;

            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return EResultCode.Success;
        }
    }
} 