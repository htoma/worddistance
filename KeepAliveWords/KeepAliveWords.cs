using System;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;

namespace KeepAliveWords
{
    public static class KeepAliveWords
    {
        private const string UrlFetchImages = "https://bingapi.azurewebsites.net/api/images";
        private const string UrlCategoryImages = "https://bingapi.azurewebsites.net/api/categoryimages";

        [FunctionName("KeepAliveWords")]
        public static async void Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer, ILogger log)
        {
            await KeepAlive.Heartbeat(UrlFetchImages, @"{""UserId"": ""keepalive"",""Prefix"": ""Keep alive""}", log);
            await KeepAlive.Heartbeat(UrlCategoryImages,
                @"{""UserId"": ""keepalive"",""Prefix"": ""Keep alive"", ""Category"": ""Happy""}", log);
        }
    }
}
