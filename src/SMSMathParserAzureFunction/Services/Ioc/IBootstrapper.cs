using Autofac;

namespace SMSMathParserAzureFunction.Services.Ioc
{
    public interface IBootstrapper
    {
        Module[] CreateModules();
    }
}