using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobTestController : ControllerBase
    {
        private readonly BlobService _blobService;

        public BlobTestController(BlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var blobName = await _blobService.UploadAsync(file);
            var url = _blobService.GetReadUrl(blobName);

            return Ok(new { blobName, url });
        }
    }
}