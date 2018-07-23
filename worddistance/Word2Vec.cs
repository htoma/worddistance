using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using NWord2Vec;

namespace worddistance
{
    public static class Word2Vec
    {
        private static readonly Model Model;

        static Word2Vec()
        {
            var env = ConfigurationManager.AppSettings["env"];
            if (env == "dev")
            {
                Model = Model.Load("deps.txt");
            }
            else
            {
                Model = Model.Load(@"D:\home\site\wwwroot\deps.txt");
            }
        }

        [FunctionName("word2vec")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string word1 = GetQueryParamValue(req, "w1");
            string word2 = GetQueryParamValue(req, "w2");

            // Find the simliarity between words
            try
            {
                var dist = Model.Distance(word1, word2);
                return req.CreateResponse(HttpStatusCode.OK, dist);
            }
            catch (Exception)
            {
                const double maxValue = 100.0;
                return req.CreateResponse(HttpStatusCode.OK, maxValue);
            }
        }

        private static string GetQueryParamValue(HttpRequestMessage req, string key)
        {
            return req.GetQueryNameValuePairs().First(x => x.Key == key).Value;
        }
    }
}
