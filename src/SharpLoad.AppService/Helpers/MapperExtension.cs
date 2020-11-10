using SharpLoad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SharpLoad.AppService.DTOs;

namespace SharpLoad.AppService.Helpers
{
    public static class MapperExtension
    {
        public static LoadTestScript ToModel(this LoadTestScriptDto dto)
        {
            return new LoadTestScript(
                dto.Id,
                dto.Name,
                new Uri(dto.BaseServerAddress),
                dto.SpawnRate,
                dto.MaxSimultaneousClients,
                dto.Status,
                dto.Requests.ToModel());
        }

        public static LoadTestScriptDto ToViewModel(this LoadTestScript model)
        {
            return new LoadTestScriptDto
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
        public static IEnumerable<LoadTestScriptDto> ToViewModelList(this IQueryable<LoadTestScript> models)
        {
            foreach (var m in models.ToArray())
                yield return m.ToViewModel();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<Request> ToModel(this IEnumerable<RequestDto> requests)
        {
            foreach (var r in requests.ToArray())
                yield return new Request(r.Id, r.Path, r.Method, r.Body, r.Headers.ToModelList());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestDto> ToViewModel(this IEnumerable<Request> requests)
        {
            foreach (var r in requests.ToArray())
                yield return new RequestDto { Id = r.Id, Path = r.Path, Body = r.Body, Headers = r.Headers.ToViewModelList() };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestHeader> ToModelList(this IEnumerable<RequestHeaderDto> requestHeaders)
        {
            foreach (var r in requestHeaders?.ToArray())
                yield return new RequestHeader{ Key = r.Key, Value = r.Value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestHeaderDto> ToViewModelList(this IEnumerable<RequestHeader> requestHeaders)
        {
            foreach (var r in requestHeaders?.ToArray())
                yield return new RequestHeaderDto { Key = r.Key, Value = r.Value };
        }
    }
}
