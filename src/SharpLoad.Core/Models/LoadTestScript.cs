using SharpLoad.Core.Enums;
using System;
using System.Collections.Generic;

namespace SharpLoad.Core.Models
{
    public class LoadTestScript : BaseModel
    {
        public string Name { get; private set; }
        public Uri BaseServerAddress { get; private set; }
        public int SpawnRate { get; private set; }
        public int MaxSimultaneousClients { get; private set; }
        public TestStatus Status { get; private set; }
        public int IntervalBetweenRequests { get; private set; }
        public virtual IEnumerable<Request> Requests { get; set; }

        public LoadTestScript(int id, string name, Uri baseServerAddress,  int maxSimultaneousClients, int spawnRate, int intervalBetweenRequests, TestStatus status, IEnumerable<Request> requests) : base(id)
        {
            Name = name;
            SpawnRate = spawnRate;
            IntervalBetweenRequests = intervalBetweenRequests;
            BaseServerAddress = baseServerAddress;
            MaxSimultaneousClients = maxSimultaneousClients;
            Status = status;
            Requests = requests;
        }
        protected LoadTestScript()
        {

        }
    }
}
