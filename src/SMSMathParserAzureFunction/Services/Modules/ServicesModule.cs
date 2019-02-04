using Autofac;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using SMSMathParserAzureFunction.Services.Logging;
using SMSMathParserAzureFunction.Services.Logging.Serilog.Sinks;

namespace SMSMathParserAzureFunction.Services.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register logger
            var loggingStorageTableName = CloudConfigurationManager.GetSetting("Logging.Storage.TableName");
            var storageConnectingString = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
            var storageAccount = CloudStorageAccount.Parse(storageConnectingString);
            builder.Register(c => new SerilogToAzureTableStorage(nameof(ServicesModule), storageAccount, loggingStorageTableName)).As<ILog>();
        }
    }
}
