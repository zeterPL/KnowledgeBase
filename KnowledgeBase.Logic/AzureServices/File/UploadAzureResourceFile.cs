using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.AzureServices.File;

public class UploadAzureResourceFile : AzureResourceFile
{
    public string ResourceName { get; }
    public string ProjectName { get; }
    public IFormFile File { get; }

    public UploadAzureResourceFile(string resourceName, string projectName, IFormFile file)
    {
        ResourceName = resourceName;
        ProjectName = projectName;
        File = file;
    }
}