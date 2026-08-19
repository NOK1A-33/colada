using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;
using Messenger.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Messanger.Api.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _UserRepository; 
        private string PasswordRegex = @"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[@!!%*?&]{8,}$)";
        private string EmailRegex = @"^(?=.{1,254})(?=.{1,64}@)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";
        public RegisterService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public async Task<EResultCode> RegValidation(string name = null, string outputName = null, string password = null, string email = null)
        {
            var users = await _UserRepository.GetAsync();
            if (name == null || outputName == null || password == null || email == null)
                return EResultCode.SomeFieldsEmpty;

            if(users.Any(u => u.Name == name || u.OutputName == outputName))
            {
                return EResultCode.Invalid_NameOROutputName;
            }

            if (Regex.IsMatch(password, PasswordRegex))
                return EResultCode.Invalid_Password;

            if (Regex.IsMatch(email, EmailRegex))
                return EResultCode.Invalid_Email;

            var result = await AddUserToDb(name, outputName, password, email);
            
            return result;
        }

        private async Task<EResultCode> AddUserToDb(string name, string outputName, string password, string email)
        {
            try 
            {  
                var passwordHasher = new PasswordHasher<User>();
                User NewUser = new User() { Id = 2, Name = name, OutputName = outputName, Email = email };
                NewUser.Password = passwordHasher.HashPassword(NewUser, password);
                UserRepository.Users2.Add(NewUser);
                await _UserRepository.AddAsync(NewUser); 
                await _UserRepository.SaveChangesAsync();

                return EResultCode.Success;
            }
            catch 
            {
                return EResultCode.DbError;
            }
        }
    }
} 