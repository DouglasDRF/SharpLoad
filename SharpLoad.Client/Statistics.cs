using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SharpLoad.Client
{
    public class Statistics
    {
        private readonly IList<double> responsesTime;
        private readonly IList<double> responsesTimeMeans;
        private LoadTestClient loadTestClient;

        public double ResponseTimeMean { get; set; }
        public uint RequestsDispatched { get; set; }
        public double MaxResponseTime { get; set; }
        public double MinResponseTime { get; set; } = double.MaxValue;
        public uint RequestsFailed { get; set; }
        public uint RequestsSucessful { get; set; }
        public uint TotalRequestsResponse { get; set; }
        public double FailRate { get; set; }
        public double ResponseRate { get; set; }

        public Statistics(LoadTestClient loadTestClient)
        {
            this.loadTestClient = loadTestClient;
            responsesTime = new List<double>();
            responsesTimeMeans = new List<double>();

            this.loadTestClient.RequestSucessful += LoadTestClient_RequestSucessful;
            this.loadTestClient.RequestFailed += LoadTestClient_RequestFailed;
            this.loadTestClient.RequestDispatch += LoadTestClient_RequestDispatch;

        }

        private void LoadTestClient_RequestDispatch(object sender, EventArgs e)
        {
            RequestsDispatched++;
        }

        private void LoadTestClient_RequestFailed(object sender, RequestResponseEventArgs e)
        {
            RequestsFailed++;
            TotalRequestsResponse++;
            responsesTime.Add(e.ResponseTimeInMiliseconds);
            MinResponseTime = e.ResponseTimeInMiliseconds < MinResponseTime ? e.ResponseTimeInMiliseconds : MinResponseTime;
            MaxResponseTime = e.ResponseTimeInMiliseconds > MaxResponseTime ? e.ResponseTimeInMiliseconds : MaxResponseTime;
        }

        private void LoadTestClient_RequestSucessful(object sender, RequestResponseEventArgs e)
        {
            RequestsSucessful++;
            TotalRequestsResponse++;
            responsesTime.Add(e.ResponseTimeInMiliseconds);
            MinResponseTime = e.ResponseTimeInMiliseconds < MinResponseTime ? e.ResponseTimeInMiliseconds : MinResponseTime;
            MaxResponseTime = e.ResponseTimeInMiliseconds > MaxResponseTime ? e.ResponseTimeInMiliseconds : MaxResponseTime;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateStatistics()
        {
            Thread t = new Thread(() => CalculateResponseTimeMean());
            t.Start();
            FailRate = Math.Round(((RequestsFailed / (double)TotalRequestsResponse) * 100), 2);
            ResponseRate = Math.Round(((TotalRequestsResponse / (double)RequestsDispatched) * 100), 2);
            t.Join();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void CalculateResponseTimeMean()
        {
            Thread.BeginCriticalRegion();
            
            double sum = 0;
            double currentMean = 0;
            double meanSum = 0;

            for (int i = 0; i < responsesTime.Count; i++)
                sum += responsesTime[i];

            currentMean = sum / responsesTime.Count;

            responsesTimeMeans.Add(currentMean);

            for (int i = 0; i < responsesTimeMeans.Count; i++)
                meanSum += responsesTimeMeans[i];

            ResponseTimeMean = Math.Truncate(meanSum / responsesTimeMeans.Count);
            
            Thread.EndCriticalRegion();

            responsesTime.Clear();
        }
    }
}
