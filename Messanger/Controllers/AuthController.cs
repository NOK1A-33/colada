using Messanger.Api.Enums;
using Messanger.DataAccess.Models;
using Messenger.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Messanger.Api.Services.Interfaces;
using Messanger.Api.ViewModels;

namespace Messanger.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _RegisterService;
        private readonly ILoginService _LoginService;
        private readonly ISearchUsersService _SearchUsersService;

        public AuthController(IRegisterService registerService, ILoginService LoginService, ISearchUsersService SearchUsersService)
        {
            _RegisterService = registerService;
            _LoginService = LoginService;
            _SearchUsersService = SearchUsersService;
        }

        [HttpGet("Users")]
        public List<User> GetUsers()
        {
            return UserRepository.Users2;
        }
        [HttpPost("register")]
        public EResultCode RegValid([FromQuery]string Name, [FromQuery] string OutputName, [FromQuery] string Email, [FromQuery] string Password)
        {
            var result = _RegisterService.RegValidation(Name, OutputName, Password, Email).Result;
            return result;
            /*switch (result)
            {
                case EResultCode.Success:
                    return Ok();
                    break;
                case EResultCode.Invalid_NameOROutputName:
                    return NoContent();
                    break;
                case EResultCode.Invalid_Password:
                    return NoContent();
                    break;
                case EResultCode.Invalid_Email:
                    return NoContent();
                    break;
                case EResultCode.SomeFieldsEmpty:
                    return NoContent();
                    break;
                default:
                    return NoContent();
                    break;
            }*/
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LogValidation([FromQuery] string Password, [FromQuery] string NameOrEmail)
        {
            var result = _LoginService.LogValidation(NameOrEmail, Password).Result;

             
            switch(result)
            {
                case EResultCode.Success:
                    return Ok();
                    break;
                case EResultCode.NotFound:
                    return Unauthorized("не авторизован");
                    break;
                default:
                    return Unauthorized("Error");
                    break;
            }
        }
        [HttpGet("me")] 
        public IActionResult IsAuthorized()
        {
            var id = HttpContext.User.FindFirst("Id")?.Value;
            if(id != null)
                return Ok(_SearchUsersService.SearchUserByIdAsync(Convert.ToInt32(id)).Result);
            return NotFound();
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var result = _LoginService.LogOut().Result;
            if (result == EResultCode.Success)
                return Ok();
            else
                return NotFound();
        }
    }
} 