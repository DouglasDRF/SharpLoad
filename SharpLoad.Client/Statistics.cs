using System.Collections.Generic;

namespace SharpLoad.Client
{
    public class Statistics
    {
        public ICollection<double> ResponsesTime { get; set; }
        public double ResponseTimeMean { get; set; }
        private LoadTestClient loadTestClient;
        public uint RequestsFailed { get; set; }
        public uint RequestsSucessful { get; set; }

        public Statistics(LoadTestClient loadTestClient)
        {
            this.loadTestClient = loadTestClient;
            ResponsesTime = new List<double>();

            this.loadTestClient.RequestSucessful += LoadTestClient_RequestSucessful;
            this.loadTestClient.RequestFailed += LoadTestClient_RequestFailed; ;

        }

        private void LoadTestClient_RequestFailed(object sender, RequestResponseEventArgs e)
        {
            RequestsFailed++;
        }

        private void LoadTestClient_RequestSucessful(object sender, RequestResponseEventArgs e)
        {
            RequestsSucessful++;
        }
    }
}
