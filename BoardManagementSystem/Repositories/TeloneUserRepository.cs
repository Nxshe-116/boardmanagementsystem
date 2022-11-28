using BoardManagementSystem.Data;
using BoardManagementSystem.Interfaces;
using BoardManagementSystem.Models;

namespace BoardManagementSystem.Repositories
{
    public class TeloneUserRepository: ITeloneUserRepository
    {

        private readonly DataContext _context;

        public TeloneUserRepository(DataContext context)
        {
            _context = context;
        }

        public ApiResponse AddTeloneUser(User user)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {

                _context.Add(user);
                apiResponse.responseObject = user;
                apiResponse.success = true;
                apiResponse.description = "New Asset Added";
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                apiResponse.success = false;
                apiResponse.description = e.Message;

            }

            return apiResponse;
        }

        public bool TeloneUserExist(string username)
        {
            return _context.TeloneUsers.Any(at => at.Username == username);
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.TeloneUsers.OrderBy(at => at.TeloneUserId).ToList();
        }
    }
}
