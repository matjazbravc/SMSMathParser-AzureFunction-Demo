using System;
using System.Collections.Generic;
using Mathos.Parser;
using Microsoft.Azure.WebJobs;
using SMSMathParserAzureFunction.Services.Ioc;
using SMSMathParserAzureFunction.Services.Logging;
using Twilio;

namespace SMSMathParserAzureFunction.Functions
{
    public static class CalculateMathExpression
    {
        [FunctionName(nameof(CalculateMathExpression))]
        public static string SendSmsChallenge(
            [ActivityTrigger] Dictionary<string, string> smsMessageValues,
            [Inject] ILog log,
            [TwilioSms(AccountSidSetting = "TwilioAccountSid", AuthTokenSetting = "TwilioAuthToken", From = "%TwilioPhoneNumber%")]
            out SMSMessage message)
        {
            log.Info("Calculating math expresion...");

            // Using: https://github.com/MathosProject/Mathos-Parser
            string result;
            try
            {
                var parser = new MathParser();
                var mathResult = parser.Parse(smsMessageValues["Body"]);
                result = $"Result: {mathResult}";
            }
            catch (Exception ex)
            {
                result = ex.Message;
                log.Error(ex.Message);
            }

            var toPhoneNumber = smsMessageValues["From"];
            message = new SMSMessage
            {
                To = toPhoneNumber,
                Body = result
            };

            // Send SMS message
            return result;
        }
    }
}