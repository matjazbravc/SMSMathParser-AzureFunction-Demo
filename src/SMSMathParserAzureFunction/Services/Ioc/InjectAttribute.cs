using System;
using Microsoft.Azure.WebJobs.Description;

namespace SMSMathParserAzureFunction.Services.Ioc
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class InjectAttribute : Attribute
    {
    }
}
