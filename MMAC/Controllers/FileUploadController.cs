// MMAC/Controllers/FileUploadController.cs
// Step 1 backend: Receives file, saves to wwwroot/uploads, returns URL

using Microsoft.AspNetCore.Mvc;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        // Allowed file types
        private static readonly string[] _allowedExtensions =
            { ".jpg", ".jpeg", ".png", ".pdf" };

        private static readonly string[] _allowedMimeTypes = {
            "image/jpeg",
            "image/png",
            "application/pdf"
        };

        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        public FileUploadController(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        // POST /api/FileUpload/HealthRecord
        [HttpPost("HealthRecord")]
        public async Task<IActionResult> UploadHealthRecord(IFormFile file)
        {
            // ── Validate 
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file received." });

            if (file.Length > MaxFileSizeBytes)
                return BadRequest(new { message = "File size must be under 5 MB." });

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(ext))
                return BadRequest(new { message = $"File type not allowed. Allowed: jpg, png, pdf" }); // Word ဖယ်လိုက်သည်

            if (!_allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
                return BadRequest(new { message = "Invalid file content type." });

            // ── Save to wwwroot/uploads/health-records/ 
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "health-records");
            Directory.CreateDirectory(uploadsFolder); // create if not exists

            // Use GUID filename to avoid conflicts and hide original name
            var uniqueFileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // ── Build public URL
            //  https://domain.com/uploads/health-records/abc123.pdf
            var baseUrl = _config["AppSettings:BaseUrl"]
                ?? $"{Request.Scheme}://{Request.Host}";
            var fileUrl = $"{baseUrl}/uploads/health-records/{uniqueFileName}";

            return Ok(new
            {
                message = "File uploaded successfully.",
                fileUrl = fileUrl,
                fileName = uniqueFileName,
                originalFileName = file.FileName //for show original name in frontend
            });
        }

        [HttpPost("DigitalRecord")]
        public async Task<IActionResult> UploadDigitalRecord(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file received." });

            if (file.Length > MaxFileSizeBytes)
                return BadRequest(new { message = "File size must be under 5 MB." });

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(ext))
                return BadRequest(new { message = $"File type not allowed. Allowed: jpg, png, pdf" });

            if (!_allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
                return BadRequest(new { message = "Invalid file content type." });

            // ── Save to wwwroot/uploads/digital-records/ ──────────────────────
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "digital-records");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var baseUrl = _config["AppSettings:BaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
            var fileUrl = $"{baseUrl}/uploads/digital-records/{uniqueFileName}";

            return Ok(new
            {
                message = "Digital record uploaded successfully.",
                fileUrl = fileUrl,
                fileName = uniqueFileName,
                originalFileName = file.FileName
            });
        }
    }
}
