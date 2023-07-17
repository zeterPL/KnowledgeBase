using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.AzureResource;

public class UpdateAzureResourceDto : AzureResourceDto, IUpdateResourceDto
{
    public Guid Id { get; set; }
}