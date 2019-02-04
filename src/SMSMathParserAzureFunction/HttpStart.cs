using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using SMSMathParserAzureFunction.Functions;
using SMSMathParserAzureFunction.Services.Ioc;
using SMSMathParserAzureFunction.Services.Logging;

namespace SMSMathParserAzureFunction
{
    public static class HttpStart
    {
        [FunctionName(nameof(HttpStart))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, methods: "post", Route = nameof(SmsMathParser))] HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClientBase starter,
            [Inject] ILog log)
        {
            var eventData = await req.Content.ReadAsStringAsync();
            var instanceId = await starter.StartNewAsync(nameof(SmsMathParser), eventData);

            log.Info($"Started orchestration with ID = '{instanceId}'.");

            var res = starter.CreateCheckStatusResponse(req, instanceId);
            res.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(10));
            return res;
        }
    }
}
