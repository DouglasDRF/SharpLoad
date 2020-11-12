using SharpLoad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
                dto.MaxSimultaneousClients,
                dto.SpawnRate,
                dto.IntervalBetweenRequests,
                dto.Status,
                dto.Requests.ToModel().ToList());
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
                IntervalBetweenRequests = model.IntervalBetweenRequests,
                Status = model.Status,
                Requests = model.Requests.ToViewModel().ToList(),
            };
        }

        public static IEnumerable<LoadTestScriptDto> ToViewModelList(this IQueryable<LoadTestScript> models)
        {
            if (models == null) yield break;

            foreach (var m in models.ToArray())
                yield return m.ToViewModel();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<Request> ToModel(this IEnumerable<RequestDto> requests)
        {
            if (requests == null) yield break;

            foreach (var r in requests.ToArray())
                yield return new Request(r.Id, r.Path, r.Method, Encoding.UTF8.GetBytes(r.Body ?? string.Empty), r.Headers.ToModelList()?.ToList(), r.ContentType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestDto> ToViewModel(this IEnumerable<Request> requests)
        {
            if (requests == null) yield break;

            foreach (var r in requests.ToArray())
                yield return new RequestDto
                { Id = r.Id, Path = r.Path, Body = Encoding.UTF8.GetString(r.Body), Headers = r.Headers.ToViewModelList()?.ToList(), ContentType = r.ContentType };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestHeader> ToModelList(this IEnumerable<RequestHeaderDto> requestHeaders)
        {
            if (requestHeaders == null) yield break;

            foreach (var r in requestHeaders.ToArray())
                yield return new RequestHeader { Key = r.Key, Value = r.Value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<RequestHeaderDto> ToViewModelList(this IEnumerable<RequestHeader> requestHeaders)
        {
            if (requestHeaders == null) yield break;

            foreach (var r in requestHeaders.ToArray())
                yield return new RequestHeaderDto { Key = r.Key, Value = r.Value };
        }
    }
}
