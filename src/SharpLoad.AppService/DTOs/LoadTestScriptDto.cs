using SharpLoad.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpLoad.AppService.DTOs
{
    public class LoadTestScriptDto : BaseDto
    {
        public LoadTestScriptDto()
        {
            Requests = new List<RequestDto>();
        }
        
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public int SpawnRate { get; set; }
        [Required]
        public string BaseServerAddress { get; set; }
        public int MaxSimultaneousClients { get; set; }
        public TestStatus Status { get; set; }
        [Required]
        public ICollection<RequestDto> Requests { get; set; }
    }
}
