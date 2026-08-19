using Messanger.Api.ViewModels;

namespace Messanger.Api.Services.Interfaces
{
    public interface ISearchUsersService
    {
        Task<List<UserViewModel>> SearchUsersAsync(string OutputName);
        Task<UserViewModel> SearchUserByIdAsync(int Id);
    }
}
