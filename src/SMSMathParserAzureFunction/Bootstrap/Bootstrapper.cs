using Autofac;
using SMSMathParserAzureFunction.Services.Ioc;
using SMSMathParserAzureFunction.Services.Modules;

namespace SMSMathParserAzureFunction.Bootstrap
{
    public class Bootstrapper : IBootstrapper
    {
        public Module[] CreateModules()
        {
            return new Module[]
            {
                new CommonCoreModule(), 
                new ServicesModule()
            };
        }
    }
}