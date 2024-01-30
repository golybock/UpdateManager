using Microsoft.AspNetCore.Mvc;
using UM.Services.Services;

namespace UM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateManagerController : ControllerBase
    {
        private readonly IVersionService _versionService;

        public UpdateManagerController(IVersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVersions()
        {
            return Ok(await _versionService.GetUpdates());
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetUpdate(string build)
        {
            try
            {
                var bytes = await _versionService.GetUpdate(build);

                return File(bytes, "application/zip");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUpdateById(Guid id)
        {
            try
            {
                var bytes = await _versionService.GetUpdate(id);

                return File(bytes, "application/zip");
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
    }
}
