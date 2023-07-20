using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.AzureResource;

public class UpdateAzureResourceDto : AzureResourceActionDto, IUpdateResourceDto
{
    public new Guid Id { get; set; }
}