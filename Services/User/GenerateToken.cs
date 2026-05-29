using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Hotel.Services.User
{
    public class GenerateToken
    {private readonly IConfiguration _configuration;

        
        
      
        public GenerateToken(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        public string token(Models.User user,List<string> roles)
        {
           
           var clamis=new List<Claim>
           {
               new Claim(ClaimTypes.NameIdentifier,user.Id),
               new Claim(ClaimTypes.Name,user.UserName),
               
           };
            foreach (var role in roles)
            {
                clamis.Add(new Claim(ClaimTypes.Role,role));

            }

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);


            var token=new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
               issuer: _configuration["jwt:issuer"],
               audience: _configuration["jwt:audience"],
                claims: clamis,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["jwt:duration"])),
                signingCredentials:creds
            );



            return  new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
