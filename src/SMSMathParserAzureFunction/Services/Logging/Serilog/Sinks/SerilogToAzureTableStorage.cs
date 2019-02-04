using Microsoft.WindowsAzure.Storage;
using Serilog;
using Serilog.Exceptions;
using SMSMathParserAzureFunction.Services.Logging.Serilog.Extensions;

namespace SMSMathParserAzureFunction.Services.Logging.Serilog.Sinks
{
    public sealed class SerilogToAzureTableStorage : LoggerBase
    {
        public SerilogToAzureTableStorage(string name, CloudStorageAccount storageAccount, string storageTableName)
        {
            Logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .WriteTo.AzureTableStorage(storageAccount, storageTableName: storageTableName)
                .CreateLogger().ForContext("SourceContext", name);
        }
    }
}