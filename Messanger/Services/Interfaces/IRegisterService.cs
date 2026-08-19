using Messanger.Api.Enums;

namespace Messanger.Api.Services.Interfaces
{
    public interface IRegisterService
    { 
        Task<EResultCode> RegValidation(string Name, string OutputName, string password, string email);  
    }
}
