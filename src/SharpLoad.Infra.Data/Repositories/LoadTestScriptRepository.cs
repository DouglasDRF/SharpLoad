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

        public async Task<LoadTestScript> GetByIdAsync(int id)
        {
            return await _context.LoadTestScripts.FindAsync(id);
        }

        public async Task<int> CreateAsync(LoadTestScript testScript)
        {
            _ = await _context.AddAsync(testScript);
            return await _context.SaveChangesAsync();
         }

        public Task<int> EditAsync(LoadTestScript testScript)
        {
            _ = _context.Update(testScript);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(LoadTestScript testScript)
        {
            _ = _context.Remove(testScript);
            return _context.SaveChangesAsync();
        }
    }
}
