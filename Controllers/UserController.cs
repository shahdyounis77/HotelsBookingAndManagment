using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.User;
using Hotel.Dtos;
using Microsoft.AspNetCore.Authorization;
namespace Hotel.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        private readonly Regsiter _regsiter;
        private readonly LoginService _login;
        private readonly GenerateToken _generateToken;
        public UserController(Regsiter regsiter, LoginService login, GenerateToken generateToken)
        {
            _regsiter = regsiter;
            _login = login;
            _generateToken = generateToken;
        }

        [HttpPost("Regsiter")]
        public async Task<IActionResult> Regsiter(UserRegsiter user)
        {
            var result = await _regsiter.Regsiteration(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "Regsitered Successfully" });
            }
            else
            {
                return BadRequest(new { message = "Regsiteration Failed", errors = result.Errors });
            }
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(UserLogin user)
        {
            var result = await _login.Login(user);

            if (result.Item1)
            {
                var token = _generateToken.token(result.Item2,result.Item3);
                return Ok(new { message = "Login Successfully", token });
            }
            else
            {
                return BadRequest(new { message = "Login Failed user not found or incorrect password" });
            }
        }
    }
}
