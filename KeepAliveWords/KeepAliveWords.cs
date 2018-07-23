using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace KeepAliveWords
{
    public static class KeepAliveWords
    {
        private const string Url = "https://worddistance.azurewebsites.net/api/word2vec?w1=breakfast&w2=bread";

        [FunctionName("KeepAliveWords")]
        public static async void Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer, ILogger log)
        {
            // call worddistance function
            var response = await HttpClientHelper.GetClient().GetAsync(Url);
            var content = await response.Content.ReadAsStringAsync();
            var msg = $"{DateTime.UtcNow}: breakfast-bread = {content}";
            log.LogInformation(msg);
        }
    }
}
