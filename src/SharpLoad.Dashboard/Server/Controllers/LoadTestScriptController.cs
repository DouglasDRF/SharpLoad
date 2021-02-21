using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpLoad.AppService.Abstractions;
using SharpLoad.AppService.DTOs;

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
        [Produces(typeof(IEnumerable<LoadTestScriptDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = (await _appService.GetAllAsync()).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LoadTestScriptDto testScript)
        {
            var rowsChanged = await _appService.CreateAsync(testScript);
            if (rowsChanged > 0)
                return Created(HttpContext.Request.Path, testScript);
            else
                return UnprocessableEntity(testScript);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var rowsChanged = await _appService.DeleteAsync(id);
            return Ok(new { rowsChanged });
        }
    }
}
