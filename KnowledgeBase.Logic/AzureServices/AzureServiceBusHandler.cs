using Azure.Messaging.ServiceBus;
using KnowledgeBase.Logic.AzureServices.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KnowledgeBase.Logic.AzureServices;

public class AzureServiceBusHandler : IAzureServiceBusHandler
{
    private readonly string _connectionString;

    public AzureServiceBusHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ServiceBus");
    }

    public async Task SendMessageAsync(string message)
    {
        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        var client = new ServiceBusClient(_connectionString, clientOptions);
        var sender = client.CreateSender("email-queue");

        var messageBatch = await sender.CreateMessageBatchAsync();
        if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
        {
            throw new Exception($"The message is too large to fit in the batch.");
        }

        try
        {
            await sender.SendMessagesAsync(messageBatch);
        }
        finally
        {
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}