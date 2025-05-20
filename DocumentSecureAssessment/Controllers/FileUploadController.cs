using System.Security.Claims;
using DocumentSecureAssessment.Data;
using DocumentSecureAssessment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentSecureAssessment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public FileUploadController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var existingFiles = _context.Documents
                .Where(f => f.FileName == file.FileName && f.UserId == userId)
                .OrderByDescending(f => f.Version);

            int newVersion = existingFiles.Any() ? existingFiles.First().Version + 1 : 0;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var doc = new Document
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Data = ms.ToArray(),
                UserId = userId,
                Version = newVersion
            };

            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new { file.FileName, doc.Version });
        }
        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFile(string fileName, [FromQuery] int? revision)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = _context.Documents
                .Where(f => f.FileName == fileName && f.UserId == userId);

            var file = revision.HasValue
                ? await query.Where(f => f.Version == revision.Value).FirstOrDefaultAsync()
                : await query.OrderByDescending(f => f.Version).FirstOrDefaultAsync();

            if (file == null)
                return NotFound("File not found");

            return File(file.Data, file.ContentType, file.FileName);
        }

        [HttpGet("my-files")]
        public async Task<IActionResult> GetMyFiles()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var files = await _context.Documents
                .Where(d => d.UserId == userId)
                .GroupBy(d => d.FileName)
                .Select(g => new {
                    FileName = g.Key,
                    LatestVersion = g.Max(f => f.Version),
                    UploadedCount = g.Count()
                }).ToListAsync();

            return Ok(files);
        }

    }
}
