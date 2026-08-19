using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;

namespace Messanger.Api.DTO
{
    public static class UserDTO
    {
        public static UserViewModel UserToViewModel(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                OutputName = user.OutputName,
                Email = user.Email
            };
        }
        public static List<UserViewModel> UsersToViewModel(List<User> users)
        {
            var result = new List<UserViewModel>();

            foreach(var user in users)
            {
                result.Add(UserToViewModel(user)); 
            }
            return result;
             

        }
    }
}

