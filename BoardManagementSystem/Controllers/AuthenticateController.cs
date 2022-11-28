using AutoMapper;
using BoardManagementSystem.Data;
using BoardManagementSystem.Interfaces;
using BoardManagementSystem.Models;
using BoardManagementSystem.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BoardManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly IConfiguration _configuration;
        private AdminAccountRepository adminAccountRepository;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly DataContext _context;
        private readonly ITeloneUserRepository _teloneUserRepository;

        private static readonly HttpClient client = new HttpClient();
    

        public AuthenticateController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IJWTManagerRepository jWTManagerRepository, DataContext context, ITeloneUserRepository teloneUserRepository)
        {

            _configuration = configuration;
            adminAccountRepository = new AdminAccountRepository(configuration);
            _jWTManager = jWTManagerRepository;
            _context = context;
            _teloneUserRepository = teloneUserRepository;



        }

        [HttpPost]
        [Route("login")]
        [EnableCors]
        public async Task<ApiResponse> LdapLogin(LoginModel model)
        {

            ApiResponse response = new ApiResponse();

            User user = new User();

            if (await adminAccountRepository.LoginAdmin(model))
            {



                try
                {
                    if (!_teloneUserRepository.TeloneUserExist(model.username))
                    {

                        user.Username = model.username;
                        user.Active = true;
                        _context.Add(user);
                        _context.SaveChanges();
                    }

                    response = _jWTManager.Authenticate(model);


                }
                catch (Exception e)
                {
                    response.success = false;

                    response.description = e.Message;
                }


            }
            else
            {
                response.success = false;

                response.description = "Unauthorized";
            }

            return response;
        }

    }
}
