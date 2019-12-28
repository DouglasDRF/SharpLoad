using SharpLoad.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpLoad.Application
{
    public class TestRunner
    {
        public uint MaxUsers { get; private set; }
        public uint SpawnRate { get; private set; }
        private LoadTestClient client;
        public TestRunner(LoadTestClient client, uint maxUsers, uint spawnRate)
        {
            this.client = client;
            MaxUsers = maxUsers;
            SpawnRate = spawnRate;
        }

        public Task RunTests()
        {

            return Task.CompletedTask;
        }
    }
}
