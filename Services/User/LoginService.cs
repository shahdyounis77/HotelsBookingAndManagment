using Microsoft.AspNetCore.Identity;
using Hotel.Dtos;
using Hotel.Models;
namespace Hotel.Services.User
{
    public class LoginService
    {
        private readonly  UserManager<Models.User> _userManager;
        public LoginService(UserManager<Models.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool, Models.User,List<string>)> Login(UserLogin user)
        {
           var checkUser = await _userManager.FindByEmailAsync(user.Email);
            var checkPassword = await _userManager.CheckPasswordAsync(checkUser, user.Password);
            var userRoles = await _userManager.GetRolesAsync(checkUser);

            if (checkUser != null && checkPassword)
            {
                return (true,checkUser,userRoles.ToList());
            }

            else
            {
               return (false,null,null);
            }
        }
    }
}
