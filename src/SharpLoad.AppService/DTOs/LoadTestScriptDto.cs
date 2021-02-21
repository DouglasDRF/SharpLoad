using SharpLoad.Core.Enums;
using System.Collections.Generic;

namespace SharpLoad.AppService.DTOs
{
    public class LoadTestScriptDto : BaseDto
    {
        public LoadTestScriptDto()
        {
            Requests ??= new List<RequestDto>();
        }
            
        public string Name { get; set; }
        public int SpawnRate { get; set; }
        public string BaseServerAddress { get; set; }
        public int MaxSimultaneousClients { get; set; }
        public int IntervalBetweenRequests { get; set; }
        public TestStatus Status { get; set; }
        public ICollection<RequestDto> Requests { get; set; }
    }
}
