namespace KnowledgeBase.Logic.AzureServices.Interfaces;

public interface IAzureServiceBusHandler
{
    Task SendMessageAsync(string message);
}