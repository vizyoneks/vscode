using System;
using System.Net.Http;

namespace dotnet.Repositories
{
    public class HttpClientFactoryService : IHttpClientFactoryService
    {
        static string baseAddress = "http://localhost:5001/";

        public HttpClient CreateClient()
        {
            var client = new HttpClient();
            SetupClientDefaults(client);
            return client;
        }

        protected virtual void SetupClientDefaults(HttpClient client)
        {
            client.Timeout = TimeSpan.FromSeconds(30); //set your own timeout.
            client.BaseAddress = new Uri(baseAddress);
        }
    }
}