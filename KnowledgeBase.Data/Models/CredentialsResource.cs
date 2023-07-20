namespace KnowledgeBase.Data.Models;

public class CredentialsResource : Resource
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Target { get; set; }
}