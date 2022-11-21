using BoardManagementSystem.Interfaces;
using BoardManagementSystem.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BoardManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDocuments : Controller
    {
        private readonly IUploadRepository _uploadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadDocuments(IWebHostEnvironment webHostEnvironment, IUploadRepository uploadRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadRepository = uploadRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadDocument(String fileName, List<IFormFile> files)
        {
            bool success = false;
            ApiResponse response = new ApiResponse();
            DocumentDetail details = new DocumentDetail();

            details.DocumentName = fileName;
            details.UplodadedOn = DateTime.Now;
            details.DocumentDescription = "";
            details.Displayname = fileName;
            if (files.Count == 0)
            {

                response.description = "Failed";
                response.success = false;

            }

            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");

            foreach (var file in files)
            {

                string filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {

                    await file.CopyToAsync(stream);
                    _uploadRepository.uploadDocument(details);
                    success = true;
                    response.responseObject = details;
                }


            }

            return Ok(success);

        }

        [HttpGet("Downloadfile")]
        public async Task<IActionResult> DownloadFile(String fileName)
        {
            try
            {
                string path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");

                string filePath = Path.Combine(path, fileName); // Here, you should validate the request and the existance of the file.

                if (filePath == null)
                {
                    return Ok(false);
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, "text/plain", Path.GetFileName(filePath));
            }
            catch (Exception)
            {

                return BadRequest("Could not get file or file does not exist");
            }

        }

    }
}
