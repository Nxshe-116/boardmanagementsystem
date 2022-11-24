using BoardManagementSystem.Models;

namespace BoardManagementSystem.Interfaces
{
    public interface ITeloneUserRepository
    {
        ApiResponse AddTeloneUser(User user);

        bool TeloneUserExist(string username);

        public ICollection<User> GetAllUsers();
    }
}
}
