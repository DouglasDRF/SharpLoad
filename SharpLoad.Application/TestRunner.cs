using SharpLoad.Application.Models;
using SharpLoad.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SharpLoad.Application
{
    public class TestRunner
    {
        private readonly TestScript testScript;
        private readonly object key = new object();

        public TestRunner(TestScript testScript)
        {
            this.testScript = testScript;
        }

        public void RunTests()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Queue<Task> usersThreads = new Queue<Task>((int)testScript.MaxUsers);
            long currentSecondReference = 0;
            LoadTestClient client = LoadTestClient.GetInstance(testScript.TargetHost);
            Statistics stats = new Statistics(client);
            client.RequestFailed += RequestError;
            client.RequestSucessful += RequestSuccessful;

            Console.WriteLine($"Load Test Script loaded for {testScript.TargetHost}");

            for (uint i = 0; i < testScript.MaxUsers; i++)
            {
                usersThreads.Enqueue(new Task(async () => await ExecuteUserRequestScriptAsync(client)));
            }

            do
            {
                if (sw.ElapsedMilliseconds - 1000 >= currentSecondReference)
                {
                    currentSecondReference = sw.ElapsedMilliseconds;

                    for (uint i = 0; i < testScript.SpawnRate; i++)
                    {
                        Task t = usersThreads.Dequeue();
                        t.Start();

                        usersThreads.Enqueue(new Task(async () => await ExecuteUserRequestScriptAsync(client)));
                    }
                }
            }
            while (sw.ElapsedMilliseconds <= testScript.TestDuration * 1000);

            sw.Stop();
            Console.WriteLine($"Finished Requests Dispatching... Waiting Remaining {usersThreads.Count} Responses...");

            Console.WriteLine($"{stats.RequestsSucessful}");
            return;
        }

        private async Task ExecuteUserRequestScriptAsync(LoadTestClient client)
        {
            foreach (UserRequestSequence request in testScript.UserRequests)
            {
                await client.SendRequestAsync(client.CreateRequestMessage(request.HttpMethod.ToString(), request.Path, client.CreateContent(request.Body, request.ApplicationType)));
            }
        }

        private void RequestError(object sender, RequestResponseEventArgs e)
        {
            Console.WriteLine($"Request {e.RequestNumber} has been failed");
        }

        private void RequestSuccessful(object sender, RequestResponseEventArgs e)
        {
            Console.WriteLine($"Request {e.RequestNumber} has been completed successfully");
        }
    }
}
