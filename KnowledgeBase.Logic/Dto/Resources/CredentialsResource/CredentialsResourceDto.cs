namespace KnowledgeBase.Logic.Dto.Resources.CredentialsResource;

public class CredentialsResourceDto : ResourceDto
{
    public string Login { get; set; }
    public string? Password { get; set; }
    public string Target { get; set; }
}