using BoardManagementSystem.Data;
using BoardManagementSystem.Interfaces;
using BoardManagementSystem.Models;


namespace HelpDeskSystem.Repository
{

    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public ApiResponse getIdByUsername(string model)
        {
            ApiResponse apiResponse = new ApiResponse();

            List<string> rolesList = new List<string>();

            try
            {

                User selectedUser = _context.TeloneUsers.Where(user => user.Username == model).FirstOrDefault();

                //   var user = _context.TeloneUsers.Where(usrRl => usrRl.Username == selectedUser!.Username).FirstOrDefault();



                apiResponse.success = true;
                apiResponse.description = "Success!";
                apiResponse.responseObject = selectedUser!.TeloneUserId;

            }
            catch (Exception)
            {

                apiResponse.success = false;
                apiResponse.description = "Something went wrong!";
            }


            return apiResponse;
        }

        public ApiResponse getRolesByUsername(string model)
        {
            ApiResponse apiResponse = new ApiResponse();

            List<string> rolesList = new List<string>();

            try
            {

                User selectedUser = _context.TeloneUsers.Where(user => user.Username == model).FirstOrDefault();

                var role = _context.TeloneUserRoles.Where(usrRl => usrRl.TeloneUserID == selectedUser!.TeloneUserId).FirstOrDefault();

                var teloneRole = _context.Roles.Where(usrRl => usrRl.TeloneRoleID == role.TeloneRoleID!).FirstOrDefault();

                apiResponse.success = true;
                apiResponse.description = "Success";
                apiResponse.responseObject = teloneRole.role!;

            }
            catch (Exception)
            {

                apiResponse.success = false;
                apiResponse.description = "Something went wrong!";
            }


            return apiResponse;
        }
    }
}
