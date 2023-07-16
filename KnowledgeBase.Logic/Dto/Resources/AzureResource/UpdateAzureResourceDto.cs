using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto.Resources.AzureResource;

public class UpdateAzureResourceDto : AzureResourceDto, IUpdateResourceDto
{
    public Guid Id { get; set; }
    public IFormFile? File { get; set; }
}