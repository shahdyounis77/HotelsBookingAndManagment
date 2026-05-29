using Microsoft.AspNetCore.Mvc;
using Hotel.Dtos;

using Microsoft.AspNetCore.Identity;

namespace Hotel.Services.User

{
    public class Regsiter
    {
        private readonly UserManager<Models.User> _userManager;

        public Regsiter(UserManager<Models.User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> Regsiteration(UserRegsiter user)
        {
            var userR = new Models.User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.FirstName + user.LastName,

                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(userR, user.Password);
            if (result.Succeeded)
            {
                var addrole = await _userManager.AddToRoleAsync(userR, "User");
            }

            return result;
        }
    }
}
