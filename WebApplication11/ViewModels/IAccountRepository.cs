using Microsoft.AspNetCore.Identity;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public interface IAccountRepository
    {
        Task<int> AdminRegistrtion(RegisterViewModel model);
        Task<int> AdminLogin(LoginViewModel user);
        Task<bool> AdminLogout();


    }
}
