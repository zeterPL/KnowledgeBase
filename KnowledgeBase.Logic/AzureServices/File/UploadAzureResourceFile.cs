using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.AzureServices.File;

public class UploadAzureResourceFile : AzureResourceFile
{
    public string ResourceName { get; }
    public Guid ProjectId { get; set; }
    public IFormFile File { get; }

    public UploadAzureResourceFile(string resourceName, Guid projectId, IFormFile file)
    {
        ResourceName = resourceName;
        ProjectId = projectId;
        File = file;
    }
}