using SharpLoad.AppService.Abstractions;
using SharpLoad.AppService.Helpers;
using SharpLoad.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpLoad.AppService.DTOs;

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

        public Task CreateAsync(LoadTestScriptDto dto)
        {
            return _repository.CreateAsync(dto.ToModel());
        }
    }
}
