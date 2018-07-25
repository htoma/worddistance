using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KeepAliveWords
{
    public class KeepAlive
    {
        public static async Task Heartbeat(string url, string payload, ILogger log)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await HttpClientHelper.GetClient().PostAsync(new Uri(url), content);
            log.LogInformation(response.StatusCode.ToString());
        }
    }
}