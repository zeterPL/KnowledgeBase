using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.CredentialsResource;

public class UpdateCredentialsResourceDto : CredentialsResourceDto, IUpdateResourceDto
{
    Guid IUpdateResourceDto.Id { get; set; }
}