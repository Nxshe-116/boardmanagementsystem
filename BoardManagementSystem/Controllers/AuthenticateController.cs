
using BoardManagementSystem.Data;
using BoardManagementSystem.Interfaces;
using BoardManagementSystem.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BoardManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AuthenticateController : ControllerBase
    {
  
        private readonly IConfiguration _configuration;
        private AdminAccountRepository adminAccountRepository;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly DataContext _context;
        private readonly ITeloneUserRepository _teloneUserRepository;


        private static readonly HttpClient client = new HttpClient();
        private readonly IMapper _mapper;



        public AuthenticateController( IConfiguration configuration,  IWebHostEnvironment webHostEnvironment, IJWTManagerRepository jWTManagerRepository, DataContext context,ITeloneUserRepository teloneUserRepository, IMapper mapper)
        {
  
            _configuration = configuration;
            adminAccountRepository = new AdminAccountRepository( configuration);
            _jWTManager= jWTManagerRepository;
            _context = context;
            _teloneUserRepository=teloneUserRepository;
            _mapper = mapper;


        }



        [HttpPost]
        [Route("adminlog")]
        [EnableCors]
        public async Task<ApiResponse> LdapLogin(LoginModel model)
        {

            ApiResponse response = new ApiResponse();

            TeloneUser user = new TeloneUser();

            if (await adminAccountRepository.LoginAdmin(model))
            {

             

                try
                {
                    if (!_teloneUserRepository.TeloneUserExist(model.username)) {

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
            else {
                response.success = false;

            response.description = "Unauthorized";
            }
        
            return response;
        }


        [HttpGet("GetAllTelOneUsers")]
        [EnableCors]
        [AllowAnonymous]

        public ApiResponse GetUsers()


        {
            ApiResponse response = new ApiResponse();
            try
            {
                var users = _mapper.Map<List<TeloneUserDto>>(_teloneUserRepository.GetAllUsers());

                response.responseObject = users;
                response.success = true;
                response.description = "Success";


            }
            catch (Exception e)
            {
                response.description = "Could not fetch users";
                response.success = false;

            }
            return response;
        }












    }
}
