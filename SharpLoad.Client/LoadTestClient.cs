using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpLoad.Client
{
    public class RequestResponseEventArgs : EventArgs
    {
        public uint RequestNumber { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpResponseMessage Response { get; set; }
    }

    public class LoadTestClient
    {
        private static HttpClient httpClient;
        private static LoadTestClient loadTestClient;
        private static uint requestsDispatched;

        public object Key { get; private set; } = new object();
        public event EventHandler<RequestResponseEventArgs> RequestFailed;
        public event EventHandler<RequestResponseEventArgs> RequestSucessful;

        private LoadTestClient(Uri host = null)
        {
            httpClient = new HttpClient();
         
            httpClient.BaseAddress = host;
            httpClient.DefaultRequestVersion = HttpVersion.Version20;
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml,application/json");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip,deflate");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36");
        }

        public static LoadTestClient GetInstance(Uri host)
        {
            if (loadTestClient == null)
                loadTestClient = new LoadTestClient(host);

            return loadTestClient;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HttpContent CreateContent(object body, string applicationType)
        {
            return new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, applicationType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HttpRequestMessage CreateRequestMessage(string method, string path, HttpContent content = null)
        {
            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod(method), path);
            message.Content = content;
            
            return message;
        }

        public Task<HttpRequestMessage> CreateRequestMessageAsync(string method, string path, HttpContent content = null)
        {
            return new Task<HttpRequestMessage>(() => CreateRequestMessage(method, path, content));
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage message)
        {
            Task<HttpResponseMessage> task = httpClient.SendAsync(message);
            
            lock (Key)
            {
                requestsDispatched++;
            }

            uint currentRequest = requestsDispatched;
            Console.WriteLine($"Dispatching Request nº {currentRequest}: " + message.Method.ToString() + " " + message.RequestUri);
            
            HttpResponseMessage response = await task;

            if (response.IsSuccessStatusCode)
                OnRequestSucessful(new RequestResponseEventArgs() { RequestNumber = currentRequest, StatusCode = response.StatusCode, Response = response });
            else
                OnRequestFailed(new RequestResponseEventArgs() { RequestNumber = requestsDispatched, StatusCode = response.StatusCode, Response = response }); ;


            return response;
        }

        protected void OnRequestFailed(RequestResponseEventArgs e)
        {
            RequestFailed?.Invoke(this, e);
        }

        protected void OnRequestSucessful(RequestResponseEventArgs e)
        {
            RequestSucessful?.Invoke(this, e);
        }
    }
}
