using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpLoad.AppService.Abstractions;
using SharpLoad.AppService.ViewModels;

namespace SharpLoad.Dashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadTestScriptController : ControllerBase
    {
        private readonly ILoadTestScriptAppService _appService;
        public LoadTestScriptController(ILoadTestScriptAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _appService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LoadTestScriptViewModel testScript)
        {
            await _appService.CreateAsync(testScript);
            return Created(HttpContext.Request.Path, testScript);
        }
    }
}
