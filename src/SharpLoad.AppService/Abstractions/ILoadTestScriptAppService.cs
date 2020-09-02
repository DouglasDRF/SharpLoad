using SharpLoad.AppService.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpLoad.AppService.Abstractions
{
    public interface ILoadTestScriptAppService
    {
        Task<IEnumerable<LoadTestScriptViewModel>> GetAllAsync();
        Task CreateAsync(LoadTestScriptViewModel viewModel);
    }
}
