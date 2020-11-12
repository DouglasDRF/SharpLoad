using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLoad.Dashboard.Client.ViewModels.Enums;

namespace SharpLoad.Dashboard.Client.ViewModels
{
    public class LoadTestScriptViewModel
    {
        public LoadTestScriptViewModel()
        {
            Requests = new List<RequestViewModel>();
        }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public int SpawnRate { get; set; }
        [Required]
        public string BaseServerAddress { get; set; }
        public int MaxSimultaneousClients { get; set; }
        public int IntervalBetweenRequests { get; set; }
        public TestStatus Status { get; set; }
        [Required]
        public ICollection<RequestViewModel> Requests { get; set; }
    }
}
