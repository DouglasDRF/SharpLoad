using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpLoad.AppService.Abstractions;
using SharpLoad.AppService.Implementations;
using SharpLoad.Core.Repositories;
using SharpLoad.Infra.Data.Contexts;
using SharpLoad.Infra.Data.Repositories;

namespace SharpLoad.CrossCutting.IoC
{
    public static class ContainerIoC
    {
        public static void InitializeDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            services.ResolveRepositories();
            services.ResolveAppService();
            
        }
        private static void ResolveRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILoadTestScriptRepository, LoadTestScriptRepository>();
        }

        private static void ResolveAppService(this IServiceCollection services)
        {
            services.AddScoped<ILoadTestScriptAppService, LoadTestScriptAppService>();
        }
    }
}
