using Microsoft.EntityFrameworkCore;
using SharpLoad.Core.Models;
using SharpLoad.Core.Repositories;
using SharpLoad.Infra.Data.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLoad.Infra.Data.Repositories
{
    public class LoadTestScriptRepository : ILoadTestScriptRepository
    {
        private readonly MainContext _context;
        public LoadTestScriptRepository(MainContext context)
        {
            _context = context;
        }

        public Task<IQueryable<LoadTestScript>> GetAllAsync()
        {
            return Task.FromResult(_context.LoadTestScripts.Include(x => x.Requests).ThenInclude(x => x.Headers).AsNoTracking().AsQueryable());
        }

        public async Task CreateAsync(LoadTestScript testScript)
        {
            _ = await _context.AddAsync(testScript);
            _ = await _context.SaveChangesAsync();
         }

        public Task EditAsync(LoadTestScript testScript)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(LoadTestScript testScript)
        {
            throw new System.NotImplementedException();
        }
    }
}
