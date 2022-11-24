using BoardManagementSystem.Data;

using BoardManagementSystem.Interfaces;
using BoardManagementSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HelpDeskSystem.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {

        private readonly IConfiguration _iconfiguration;

        private readonly IRoleRepository _RoleRepository;

        private readonly DataContext _context;
        public JWTManagerRepository(IConfiguration iconfiguration, DataContext context, IRoleRepository roleRepository)
        {
            _iconfiguration = iconfiguration;
            _context = context;

            _RoleRepository = roleRepository;

        }
        public ApiResponse Authenticate(LoginModel model)
        {
            ApiResponse response = new ApiResponse();


            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken? token = null;
            var loggedinUser = _context.TeloneUsers.Where(obj => obj.Username == model.username).FirstOrDefault();
            var assignedrole = _context.TeloneUserRoles.Where(obj => obj.TeloneUserID == loggedinUser!.TeloneUserId).FirstOrDefault();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var role = _context.TeloneUserRoles.Where(obj => obj.TeloneUserID == loggedinUser.TeloneUserId).FirstOrDefault() == null ? "User" : _context.Roles.Where(obj => obj.TeloneRoleID == assignedrole.TeloneRoleID).FirstOrDefault().role.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                  new Claim(ClaimTypes.Name, model.username),
                  new Claim("id", loggedinUser!.TeloneUserId.ToString()),
                  new Claim("email", model.username + "@telone.co.zw"),
                //  new Claim(ClaimTypes.Role, role),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            token = tokenHandler.CreateToken(tokenDescriptor);


            response.success = true;
            response.description = "Successful";

            response.responseObject = tokenHandler.WriteToken(token);



            return response;
        }
    }
}


