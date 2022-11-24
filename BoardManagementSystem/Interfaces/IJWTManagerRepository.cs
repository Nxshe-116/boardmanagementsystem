using BoardManagementSystem.Models;


namespace BoardManagementSystem.Interfaces
{
    public interface IJWTManagerRepository
    {

        ApiResponse Authenticate(LoginModel model);
    }
}
