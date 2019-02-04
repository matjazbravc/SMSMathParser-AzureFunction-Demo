using System.Collections.Generic;

namespace SMSMathParserAzureFunction.Services.Helpers
{
    public interface IFileHelper
    {
        List<string> GetAllFilesFromDirectory(string root, bool searchSubfolders = true);
    }
}
