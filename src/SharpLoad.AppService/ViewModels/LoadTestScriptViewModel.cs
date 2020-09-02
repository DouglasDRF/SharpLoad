using SharpLoad.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpLoad.AppService.ViewModels
{
    public class LoadTestScriptViewModel : BaseViewModel
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
        public TestStatus Status { get; set; }
        [Required]
        public ICollection<RequestViewModel> Requests { get; set; }
    }
}
