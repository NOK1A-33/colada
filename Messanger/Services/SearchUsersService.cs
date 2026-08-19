using Messanger.Api.DTO;
using Messanger.Api.Services.Interfaces;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Xml.Linq;

namespace Messanger.Api.Services
{
    public class SearchUsersService : ISearchUsersService
    {

        private readonly IUserRepository _UserRepository;

        public SearchUsersService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task<List<UserViewModel>> SearchUsersAsync(string OutputName)
        {
            var users = _UserRepository.GetAsync().Result;
            var RawResult = users.Where(u => Search(u.OutputName, OutputName)).ToList();
            
            if (RawResult.Count > 0)
            {
                var usersviewmodel = UserDTO.UsersToViewModel(RawResult);

                return usersviewmodel;
            }
            else
                return new List<UserViewModel>();
        }
        public async Task<UserViewModel> SearchUserByIdAsync(int Id)
        {
            var users = _UserRepository.GetAsync().Result;

            var RawResult = users.Where(u => u.Id == Id).FirstOrDefault();
            if (RawResult != null)
            {
                var userviewmodel = UserDTO.UserToViewModel(RawResult);

                return userviewmodel;
            }
            else
                return new UserViewModel();
        }
        public bool Search(string Name, string OtherName)
        {
            if (Name.Equals(OtherName, StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }
    }
} 