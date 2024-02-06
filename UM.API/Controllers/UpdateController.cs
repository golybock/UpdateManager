using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;
using UM.Models.Files.Blank;
using UM.Services.Services;

namespace UM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IVersionService _versionService;

        public UpdateController(IVersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetVersions()
        {
            return Ok(await _versionService.GetUpdates());
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> DownloadUpdate(string build)
        {
            try
            {
                var bytes = await _versionService.GetUpdate(build);

                return File(bytes, "application/x-zip-compressed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DownloadUpdateById(Guid id)
        {
            try
            {
                var bytes = await _versionService.GetUpdate(id);

                return File(bytes, "application/x-zip-compressed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastUpdate()
        {
            try
            {
                var version = await _versionService.GetLastUpdate();

                return Ok(version);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> UploadUpdate(IFormFile formFile)
        {
            if (formFile.ContentType != "application/x-zip-compressed")
                throw new FileFormatException("available only zip");

            var filePath = $"Updates/{formFile.FileName}";

            // save file
            await using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                await formFile.CopyToAsync(streamWriter.BaseStream);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateVersion(VersionBlank versionBlank)
        {
            try
            {
                var result = await _versionService.CreateVersion(versionBlank);

                return result ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e.Message);
            }
        }
    }
}
