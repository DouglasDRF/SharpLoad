using SharpLoad.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLoad.Core.Repositories
{
    public interface ILoadTestScriptRepository
    {
        Task<IQueryable<LoadTestScript>> GetAllAsync();
        Task<LoadTestScript> GetByIdAsync(int id);
        Task<int> CreateAsync(LoadTestScript testScript);
        Task<int> EditAsync(LoadTestScript testScript);
        Task<int> DeleteAsync(LoadTestScript testScript);
    }
}
