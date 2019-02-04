using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using SMSMathParserAzureFunction.Services.Ioc;
using SMSMathParserAzureFunction.Services.Logging;

namespace SMSMathParserAzureFunction.Functions
{
    /* 
     * To run this sample, you'll need to define the following app settings:
     *
     *   - TwilioAccountSid: your Twilio account's SID
     *   - TwilioAuthToken: your Twilio account's auth token
     *   - TwilioPhoneNumber: an SMS-capable Twilio number
     *
     * For Twilio trial accounts, you also need to verify the phone number in your MonitorRequest.
     *
     * Twilio: https://www.twilio.com
     *
     * Twilio binding for Azure Functions
     * https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-twilio#c-example
     *
     * How to setup Ngrok with Twilio and test function locally:
     * https://www.twilio.com/blog/2017/01/how-to-send-daily-sms-reminders-using-c-azure-functions-and-twilio.html
     * https://www.twilio.com/docs/guides/serverless-webhooks-azure-functions-and-csharp
     *
     * Setup WebHook
     * https://www.twilio.com/console/phone-numbers/incoming
     *
     * Sends a SMS text message when triggered by a queue message
     * https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-twilio#c-example
     */

    public static class SmsMathParser
    {
        [FunctionName(nameof(SmsMathParser))]
        public static async Task<string> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context,
            [Inject] ILog log)
        {
            var input = context.GetInput<string>();
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Input is required");
            }

            log.Info($"Parsing {input} input");
            var smsMessageValues = ParseInput(input);
            
            // Call Math parser activity function
            var result = await context.CallActivityAsync<string>(nameof(CalculateMathExpression), smsMessageValues);
            return result;
        }

        private static Dictionary<string, string> ParseInput(string input)
        {
            var normalizedInput = input.Replace("%2B", "+").Replace("%3B", "=").Replace("%2F", "/");
            // Split it to the Dictionary
            var smsMessageValues = normalizedInput.Split('&')
                .Select(value => value.Split('='))
                .ToDictionary(pair => Uri.UnescapeDataString(pair[0]), pair => Uri.UnescapeDataString(pair[1]));
            return smsMessageValues;
        }
    }
}