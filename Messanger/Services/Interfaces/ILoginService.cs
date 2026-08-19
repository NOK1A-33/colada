using Messanger.Api.Enums;

namespace Messanger.Api.Services.Interfaces
{
    public interface ILoginService
    {
        Task<EResultCode> LogValidation(string NameOrEmail, string password);
        Task<EResultCode> LogOut();
    }
}
