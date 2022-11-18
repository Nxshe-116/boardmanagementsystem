using BoardManagementSystem.Models;

namespace BoardManagementSystem.Interfaces
{
    public interface IUploadRepository
    {

        public ApiResponse uploadDocument(DocumentDetail document);

    }
}
