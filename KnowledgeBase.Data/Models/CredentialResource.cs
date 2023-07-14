namespace KnowledgeBase.Data.Models;

public class CredentialResource : Resource
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Where { get; set; }
}