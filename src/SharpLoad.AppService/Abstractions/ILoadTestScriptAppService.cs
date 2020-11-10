using System.Collections.Generic;
using System.Threading.Tasks;
using SharpLoad.AppService.DTOs;

namespace SharpLoad.AppService.Abstractions
{
    public interface ILoadTestScriptAppService
    {
        Task<IEnumerable<LoadTestScriptDto>> GetAllAsync();
        Task CreateAsync(LoadTestScriptDto dto);
    }
}
