using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpLoad.Client
{
    public class LoadTestClient
    {
        private static LoadTestClient m_Client;
        protected static HttpClient HttpClient { get; private set; }
                
        uint requestsDispatched;
        uint requestsSucessful;
        uint requestsFailed;
        ICollection<string> errorMessages;
        
        private LoadTestClient(string host)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = host.Contains("http") ? new Uri(host) : new Uri("http://" + host);
        }

        public static LoadTestClient GetInstance(string host)
        {
            if (m_Client != null)
            {
                HttpClient.BaseAddress = host.Contains("http") ? new Uri(host) : new Uri("http://" + host);
                return m_Client;
            }

            else
                return new LoadTestClient(host);
        }

        public Task Post(HttpContent content)
        {
            return null; ;
        }
    }
}
