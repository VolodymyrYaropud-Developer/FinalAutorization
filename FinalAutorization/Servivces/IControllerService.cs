using FinalAutorization.Models;

namespace FinalAutorization.Servivces
{
    public interface IControllerService
    {
        Task<Response> IsLoginSuccess(LoginModel loginModel);
        Task<Response> IsRegisterSuccess(RegisterModel registerModel);
    }
}
