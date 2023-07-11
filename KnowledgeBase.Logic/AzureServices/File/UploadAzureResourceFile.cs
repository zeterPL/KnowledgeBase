using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.AzureServices.File;

public class UploadAzureResourceFile : AzureResourceFile
{
    public string ResourceName { get; set; }
    public string ProjectName { get; set; }
    public IFormFile File { get; init; }

    public UploadAzureResourceFile(string resourceName, string projectName, IFormFile file)
    {
        ResourceName = resourceName;
        ProjectName = projectName;
        File = file;
    }
}