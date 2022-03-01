using Microsoft.AspNetCore.Identity;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public class AccountViewModel : IAccountViewModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountViewModel(UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<int> AdminRegistrtion(RegisterModel model)
        {

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return 1;
            }
            return 0;
        }

        public async Task<int> AdminLogin(LoginModel user)
        {
           
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return 1;
                }
                return 0;

        }

        public async Task<bool> AdminLogout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }



    }
}

