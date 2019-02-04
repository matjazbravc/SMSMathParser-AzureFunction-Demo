using System.ComponentModel;

namespace SMSMathParserAzureFunction.Services.Enums
{
    public enum EncodingType
    {
        [Description("gzip")]
        Gzip,
        [Description("deflate")]
        Deflate
    }
}
