using System;
using System.Configuration;
using System.Net.Http;

namespace KeepAliveWords
{
    public static class HttpClientHelper
    {
        private const string HeaderName = "x-functions-key";

        private static readonly Lazy<HttpClient> Client = new Lazy<HttpClient>(() => new HttpClient());

        public static HttpClient GetClient()
        {
            if (!Client.Value.DefaultRequestHeaders.Contains(HeaderName))
            {
                Client.Value.DefaultRequestHeaders.Add(HeaderName, ConfigurationManager.AppSettings["FunctionHeader"]);
            }

            return Client.Value;
        }
    }
}