using BoardManagementSystem.Data;
using BoardManagementSystem.Interfaces;
using BoardManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardManagementSystem.Repositories
{
    public class UploadRepository : IUploadRepository
    {
        private readonly DataContext _context;


        public UploadRepository(DataContext context)
        {

            _context = context;


        }
        public ApiResponse uploadDocument(DocumentDetail document)
        {
         ApiResponse apiResponse= new ApiResponse();

            try
            {
                _context.Add(document);
                apiResponse.responseObject = document;
                apiResponse.success = true;
                apiResponse.description = "Document Uploaded";
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                apiResponse.success = false;
                apiResponse.description = e.Message;
            }

            return apiResponse;
        }
    }
}
