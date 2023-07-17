using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.CredentialsResource;

public class UpdateCredentialsResourceDto : CredentialsResourceDto, IUpdateResourceDto
{
    public new Guid Id { get; set; }
}