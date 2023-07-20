using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto.Resources.AzureResource;

public class AzureResourceDto : ResourceDto
{
    public string? AzureStorageAbsolutePath { get; set; }
    public string? AzureFileName { get; set; }
    public IFormFile? File { get; set; }
}