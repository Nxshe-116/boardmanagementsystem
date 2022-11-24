using BoardManagementSystem.Models;

namespace BoardManagementSystem.Interfaces
{
    public interface IRoleRepository
    {
        public ApiResponse getRolesByUsername(string model);

        public ApiResponse getIdByUsername(string model);

    }
}
