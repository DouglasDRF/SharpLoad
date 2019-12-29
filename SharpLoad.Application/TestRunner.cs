using SharpLoad.Application.Models;
using SharpLoad.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void RunTests(bool verbose = true, uint updateTime = 3)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Queue<Task> usersThreads = new Queue<Task>((int)testScript.MaxUsers);
            double currentSecondReference = 0;
            LoadTestClient client = LoadTestClient.GetInstance(testScript.TargetHost);
            Statistics stats = new Statistics(client);

            if (verbose)
            {
                client.RequestFailed += RequestError;
                client.RequestSucessful += RequestSuccessful;
            }

            Console.WriteLine($"Load Test Script loaded for {testScript.TargetHost}");

            for (int i = 0; i < testScript.MaxUsers; i++)
                usersThreads.Enqueue(new Task(() => ExecuteUserRequestScript(client)));

            do
            {
                if (sw.Elapsed.TotalSeconds - 1 >= currentSecondReference)
                {
                    currentSecondReference = sw.Elapsed.TotalSeconds;

                    for (int i = 0; i < testScript.SpawnRate; i++)
                    {
                        Task t = usersThreads.Dequeue();
                        t.Start();
                        usersThreads.Enqueue(new Task(() => ExecuteUserRequestScript(client)));
                    }
                }

            }
            while (sw.Elapsed.TotalSeconds <= testScript.TestDuration);

            sw.Stop();
            Console.WriteLine($"Finished Requests Dispatching... Waiting Remaining Responses...");
            _ = Task.WhenAny(usersThreads.ToArray());
            usersThreads.Clear();

            Console.WriteLine("\n\n ================================== RESULTS =================================");
            Console.WriteLine($"All Requests Sent: {stats.RequestsSucessful + stats.RequestsFailed}");
            Console.WriteLine($"All Requests Sucessful: {stats.RequestsSucessful}");
            Console.WriteLine($"All Requests Failed: {stats.RequestsFailed}");
            Console.WriteLine($"Fail Rate: {stats.RequestsFailed / (stats.RequestsSucessful + stats.RequestsFailed)}");
            Console.WriteLine($"Response Time Mean: {stats.ResponseTimeMean}");

            return;
        }

        private void ExecuteUserRequestScript(LoadTestClient client)
        {
            foreach (UserRequestSequence request in testScript.UserRequests)
                _ = client.SendRequestAsync(client.CreateRequestMessage(request.HttpMethod.ToString(), request.Path, client.CreateContent(request.Body, request.ApplicationType))).ConfigureAwait(false);
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
