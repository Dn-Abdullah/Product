using Microsoft.AspNetCore.Identity;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public interface IAccountViewModel
    {
        Task<int> AdminRegistrtion(RegisterModel model);
        Task<int> AdminLogin(LoginModel user);
        Task<bool> AdminLogout();


    }
}
