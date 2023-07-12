using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto;

public class ResourceDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public ResourceCategory Category { get; set; }
    public Guid UserId { get; set; }
    public string? AzureStorageAbsolutePath { get; set; }
    public string? AzureFileName { get; set; }
    public IFormFile? File { get; set; }
}