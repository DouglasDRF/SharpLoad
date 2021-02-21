using System.Collections.Generic;
using System.Threading.Tasks;
using SharpLoad.AppService.Abstractions;
using SharpLoad.AppService.Helpers;
using SharpLoad.AppService.DTOs;
using SharpLoad.Core.Repositories;

namespace SharpLoad.AppService.Implementations
{
    public class LoadTestScriptAppService : ILoadTestScriptAppService
    {
        private readonly ILoadTestScriptRepository _repository;
        public LoadTestScriptAppService(ILoadTestScriptRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoadTestScriptDto>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).ToViewModelList();
        }

        public Task<int> CreateAsync(LoadTestScriptDto dto)
        {
            return _repository.CreateAsync(dto.ToModel());
        }

        public Task<int> UpdateAsync(LoadTestScriptDto dto)
        {
             return _repository.EditAsync(dto.ToModel());
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entry = await _repository.GetByIdAsync(id);
            
            if (entry != null)
                return await _repository.DeleteAsync(entry);

            return 0;
        }
    }
}
