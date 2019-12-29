using SharpLoad.Application.Models;
using SharpLoad.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public void RunTests(bool verbose = false, uint updateTime = 3)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Queue<Task> usersThreads = new Queue<Task>((int)testScript.MaxUsers);
            double currentSecondRequest = 1;
            double currentSecondUpdate = 5;
            LoadTestClient client = LoadTestClient.GetInstance(testScript.TargetHost);
            Statistics stats = new Statistics(client);

            if (verbose)
            {
                client.RequestFailed += RequestError;
                client.RequestSucessful += RequestSuccessful;
            }

            Console.WriteLine($"Load Test Script loaded for {testScript.TargetHost}");

            for (int i = 0; i < testScript.MaxUsers; i++)
                usersThreads.Enqueue(new Task(() => ExecuteUserRequestScript(client, verbose)));

            do
            {
                if (sw.Elapsed.TotalSeconds >= currentSecondRequest)
                {
                    currentSecondRequest++;

                    for (int i = 0; i < testScript.SpawnRate; i++)
                    {
                        Task t = usersThreads.Dequeue();
                        t.Start();
                        usersThreads.Enqueue(new Task(() => ExecuteUserRequestScript(client, verbose)));
                    }
                }
                if (sw.Elapsed.TotalSeconds >= currentSecondUpdate)
                {
                    currentSecondUpdate += 5;
                    stats.UpdateStatistics();

                    Console.WriteLine("\n\n ================================== PARTIAL RESULTS =================================\n");
                    ShowResults(stats, sw.Elapsed.TotalSeconds);
                }

            }
            while (sw.Elapsed.TotalSeconds <= testScript.TestDuration);

            Console.WriteLine($"\n\nFinished Requests Dispatching... Waiting Remaining Responses...");

            Task[] lastBatch = usersThreads.Take((int)testScript.SpawnRate).ToArray();

            
            foreach (var t in lastBatch)
                t.Start();

            Task.WaitAll(lastBatch);
            sw.Stop();
            Thread.Yield();
            stats.UpdateStatistics();
            Console.WriteLine("\n\n ================================== RESULTS =================================\n");
            ShowResults(stats, sw.Elapsed.TotalSeconds);

            return;
        }

        private void ExecuteUserRequestScript(LoadTestClient client, bool verbose)
        {
            foreach (UserRequestSequence request in testScript.UserRequests)
                _ = client.SendRequestAsync(client.CreateRequestMessage(request.HttpMethod.ToString(), request.Path, client.CreateContent(request.Body, request.ApplicationType)), verbose).ConfigureAwait(false);
        }

        private void RequestError(object sender, RequestResponseEventArgs e)
        {
            Console.WriteLine($"Request {e.RequestNumber} has been failed");
        }

        private void RequestSuccessful(object sender, RequestResponseEventArgs e)
        {
            Console.WriteLine($"Request {e.RequestNumber} has been completed successfully");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ShowResults(Statistics stats, double elapsedSeconds)
        {
            Console.WriteLine($"\tTest Elapsed Time: {Math.Round(elapsedSeconds, 2)} s");
            Console.WriteLine($"\tRequests per second: {Math.Round(stats.RequestsDispatched / elapsedSeconds, 0)} \n");

            Console.WriteLine($"\tAll Requests Sent: {stats.RequestsDispatched}");
            Console.WriteLine($"\tAll Requests Responded: {stats.TotalRequestsResponse}");
            Console.WriteLine($"\tAll Requests Sucessful: {stats.RequestsSucessful}");
            Console.WriteLine($"\tAll Requests Failed: {stats.RequestsFailed}\n");
            
            Console.WriteLine($"\tFail Rate: {stats.FailRate}%");
            Console.WriteLine($"\tResponse Rate: {stats.ResponseRate}%\n");
            
            Console.WriteLine($"\tResponse Time Mean: {stats.ResponseTimeMean} ms");
            Console.WriteLine($"\tMinimum Response Time: {stats.MinResponseTime} ms");
            Console.WriteLine($"\tMaximum Time Mean: {stats.MaxResponseTime} ms");
        }
    }
}
