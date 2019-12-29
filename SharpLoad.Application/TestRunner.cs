using SharpLoad.Application.Models;
using SharpLoad.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SharpLoad.Application
{
    public class TestRunner
    {
        private readonly TestScript testScript;
        private uint requestNumber;

        public TestRunner(TestScript testScript)
        {
            this.testScript = testScript;
        }

        public void RunTests()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Queue<Task> usersThreads = new Queue<Task>((int)testScript.MaxUsers);
            long currentSecondReference = 0;
            LoadTestClient client = new LoadTestClient(testScript.TargetHost);

            for (uint i = 0; i < testScript.MaxUsers; i++)
            {
                usersThreads.Enqueue(new Task(() => ExecuteUserRequestScript(client) ));
            }

            while (sw.ElapsedMilliseconds <= testScript.TestDuration * 1000)
            {
                if (sw.ElapsedMilliseconds - 1000 >= currentSecondReference)
                {
                    currentSecondReference = sw.ElapsedMilliseconds;

                    for (uint i = 0; i < testScript.SpawnRate; i++)
                    {
                        Task t = usersThreads.Dequeue();
                        t.Start();
                        usersThreads.Enqueue(new Task(() => ExecuteUserRequestScript(client)));
                    }
                }
            }
            return;
        }

        private void ExecuteUserRequestScript(LoadTestClient client)
        {
            foreach (UserRequestSequence reqSeq in testScript.UserRequestSequences)
            {
                client.RequestFailed += RequestError;
                client.RequestFailed += RequestSuccessful;
                _ = client.SendRequestAsync(client.CreateRequestMessage(reqSeq.HttpMethod.ToString(), reqSeq.Path, client.CreateContent(reqSeq.Body, reqSeq.ApplicationType)));
            }
        }

        private void RequestError(object sender, EventArgs e)
        {
            Console.WriteLine("Request has been failed");
        }

        private void RequestSuccessful(object sender, EventArgs e)
        {
            Console.WriteLine("Request has been completed successfully");
        }
    }
}
