using SharpLoad.AppService.ViewModels;
using SharpLoad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SharpLoad.AppService.Helpers
{
    public static class MapperExtension
    {
        public static LoadTestScript ToModel(this LoadTestScriptViewModel viewModel)
        {
            return new LoadTestScript(
                viewModel.Id,
                viewModel.Name,
                new Uri(viewModel.BaseServerAddress),
                viewModel.SpawnRate,
                viewModel.MaxSimultaneousClients,
                viewModel.Status,
                viewModel.Requests.ToModel());
        }

        public static LoadTestScriptViewModel ToViewModel(this LoadTestScript model)
        {
            return new LoadTestScriptViewModel
            {
                Id = model.Id,
                Name = model.Name,
                BaseServerAddress = model.BaseServerAddress.ToString(),
                SpawnRate = model.SpawnRate,
                MaxSimultaneousClients = model.MaxSimultaneousClients,
                Status = model.Status,
                Requests = model.Requests.ToViewModel().ToList(),
            };
        }
        public static IEnumerable<LoadTestScriptViewModel> ToViewModelList(this IQueryable<LoadTestScript> models)
        {
            foreach (var m in models.ToArray())
                yield return m.ToViewModel();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<Request> ToModel(this IEnumerable<RequestViewModel> requests)
        {
            foreach (var r in requests.ToArray())
                yield return new Request(r.Id, r.Path, r.Method, r.Body, r.Headers.ToModelList());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestViewModel> ToViewModel(this IEnumerable<Request> requests)
        {
            foreach (var r in requests.ToArray())
                yield return new RequestViewModel { Id = r.Id, Path = r.Path, Body = r.Body, Headers = r.Headers.ToViewModelList() };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestHeader> ToModelList(this IEnumerable<RequestHeaderViewModel> requestHeaders)
        {
            foreach (var r in requestHeaders?.ToArray())
                yield return new RequestHeader{ Key = r.Key, Value = r.Value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestHeaderViewModel> ToViewModelList(this IEnumerable<RequestHeader> requestHeaders)
        {
            foreach (var r in requestHeaders?.ToArray())
                yield return new RequestHeaderViewModel { Key = r.Key, Value = r.Value };
        }
    }
}
