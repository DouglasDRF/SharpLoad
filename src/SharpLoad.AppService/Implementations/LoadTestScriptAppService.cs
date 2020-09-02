using SharpLoad.AppService.Abstractions;
using SharpLoad.AppService.Helpers;
using SharpLoad.AppService.ViewModels;
using SharpLoad.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpLoad.AppService.Implementations
{
    public class LoadTestScriptAppService : ILoadTestScriptAppService
    {
        private readonly ILoadTestScriptRepository _repository;
        public LoadTestScriptAppService(ILoadTestScriptRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoadTestScriptViewModel>> GetAllAsync()
        {
           return (await _repository.GetAllAsync()).ToViewModelList();
        }

        public Task CreateAsync(LoadTestScriptViewModel viewModel)
        {
            return _repository.CreateAsync(viewModel.ToModel());
        }
    }
}
