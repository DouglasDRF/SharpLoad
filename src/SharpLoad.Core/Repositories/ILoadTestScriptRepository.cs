using SharpLoad.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLoad.Core.Repositories
{
    public interface ILoadTestScriptRepository
    {
        Task<IQueryable<LoadTestScript>> GetAllAsync();
        Task CreateAsync(LoadTestScript testScript);
        Task EditAsync(LoadTestScript testScript);
        Task DeleteAsync(LoadTestScript testScript);
    }
}
