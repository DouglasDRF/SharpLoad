using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpLoad.Client
{
    public class LoadTestClient
    {
        private static HttpClient httpClient;
        private static uint requestsDispatched;
        public static object Key { get; set; }
        public static uint Errors { get; set; }
        public static uint Success { get; set; }

        public event EventHandler RequestFailed;
        public event EventHandler RequestSucessful;

        public LoadTestClient(Uri host)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = host;
            
            Key = new object();
            Errors = 0;
            Success = 0;
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
            message.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml,application/json");
            message.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            message.Headers.TryAddWithoutValidation("User-Agent", "SharpLoad/0.1");
            
            return message;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HttpResponseMessage SendRequest(HttpRequestMessage message)
        {
            return httpClient.SendAsync(message).Result;
        }

        public Task<HttpRequestMessage> CreateRequestMessageAsync(string method, string path, HttpContent content = null)
        {
            return new Task<HttpRequestMessage>(() => CreateRequestMessage(method, path, content));
        }

        public Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage message)
        {
            lock (Key)
            {
                requestsDispatched++;
                Console.WriteLine($"Dispatching Request nº {requestsDispatched}: " + message.Method.ToString() + message.RequestUri.ToString());
            }
            
            return httpClient.SendAsync(message).ContinueWith((task) => ValidateRequest(task.Result) );
        }

        private HttpResponseMessage ValidateRequest(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                lock (Key)
                {
                    Success++;
                    OnRequestSucessful(EventArgs.Empty);
                }
                
                return response;
            }
            else
            {
                Errors++;
                OnRequestFailed(EventArgs.Empty);
                return response;
            }
        }

        public void OnRequestFailed(EventArgs e)
        {
            RequestFailed?.Invoke(this, e);
        }

        public void OnRequestSucessful(EventArgs e)
        {
            RequestSucessful?.Invoke(this, e);
        }
    }
}
