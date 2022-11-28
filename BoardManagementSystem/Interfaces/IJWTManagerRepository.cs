using BoardManagementSystem.Models;

namespace BoardManagementSystem.Interfaces
{
    public interface IJWTManagerRepository
    {
     public  ApiResponse Authenticate(LoginModel model);
    }
}
