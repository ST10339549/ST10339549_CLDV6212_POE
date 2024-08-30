using Microsoft.AspNetCore.Mvc;
using ST10339549_CLDV6212_POE.Services;

namespace ST10339549_CLDV6212_POE.Controllers
{
    public class FileStorageController : Controller
    {
        private readonly FileStorageService _fileStorageService;

        public FileStorageController()
        {
            string connectionString = "";
            _fileStorageService = new FileStorageService(connectionString);
        }

        public async Task<IActionResult> Index()
        {
            var filesList = await _fileStorageService.ListFilesAsync();
            return View(filesList);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                await _fileStorageService.UploadFileAsync(file);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
