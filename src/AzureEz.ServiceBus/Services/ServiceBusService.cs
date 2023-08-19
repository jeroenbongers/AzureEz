using AzureEz.ServiceBus.Infrastructure.Interfaces;
using AzureEz.ServiceBus.Services.Interfaces;

namespace AzureEz.ServiceBus.Services;

public class ServiceBusService : IServiceBusService
{
    private readonly IServiceBusFactory _serviceBusFactory;

    public ServiceBusService(IServiceBusFactory serviceBusFactory)
    {
        _serviceBusFactory = serviceBusFactory;
    }

    /// <inheritdocs/>
    public async Task SendMessageAsync(string queueOrTopicName, ServiceBusMessage message, CancellationToken cancellationToken = default)
    {
        await _serviceBusFactory.GetServiceBusSender(queueOrTopicName).SendMessageAsync(message, cancellationToken);
    }

    /// <inheritdocs/>
    public async Task SendMessageAsync(string queueOrTopicName, object message, TimeSpan? delay = null, CancellationToken cancellationToken = default)
    {
        var binaryData = BinaryData.FromObjectAsJson(message);
        var serviceBusMessage = new ServiceBusMessage(binaryData);
        if (delay != null)
        {
            serviceBusMessage.ScheduledEnqueueTime = DateTimeOffset.UtcNow.Add(delay.Value);
        }

        await _serviceBusFactory.GetServiceBusSender(queueOrTopicName).SendMessageAsync(serviceBusMessage, cancellationToken);
    }

    /// <inheritdocs/>
    public async Task SendMessageAsync(string queueOrTopicName, byte[] message, TimeSpan? delay = null, CancellationToken cancellationToken = default)
    {
        var serviceBusMessage = new ServiceBusMessage(message);
        if (delay != null)
        {
            serviceBusMessage.ScheduledEnqueueTime = DateTimeOffset.UtcNow.Add(delay.Value);
        }

        await _serviceBusFactory.GetServiceBusSender(queueOrTopicName).SendMessageAsync(serviceBusMessage, cancellationToken);
    }
    
    /// <inheritdocs/>
    public async Task SendMessageAsync(string queueOrTopicName, string message, TimeSpan? delay = null, CancellationToken cancellationToken = default)
    {
        var serviceBusMessage = new ServiceBusMessage(message);
        if (delay != null)
        {
            serviceBusMessage.ScheduledEnqueueTime = DateTimeOffset.UtcNow.Add(delay.Value);
        }

        await _serviceBusFactory.GetServiceBusSender(queueOrTopicName).SendMessageAsync(serviceBusMessage, cancellationToken);
    }

    /// <inheritdocs/>
    public async Task SendMessageAsync(string queueOrTopicName, object message, DateTimeOffset? dateTimeOffset = null, CancellationToken cancellationToken = default)
    {
        var binaryData = BinaryData.FromObjectAsJson(message);
        var serviceBusMessage = new ServiceBusMessage(binaryData);
        if (dateTimeOffset != null)
        {
            serviceBusMessage.ScheduledEnqueueTime = dateTimeOffset.Value;
        }

        await _serviceBusFactory.GetServiceBusSender(queueOrTopicName).SendMessageAsync(serviceBusMessage, cancellationToken);
    }

    /// <inheritdocs/>
    public async Task SendMessageAsync(string queueOrTopicName, byte[] message, DateTimeOffset? dateTimeOffset = null, CancellationToken cancellationToken = default)
    {
        var serviceBusMessage = new ServiceBusMessage(message);
        if (dateTimeOffset != null)
        {
            serviceBusMessage.ScheduledEnqueueTime = dateTimeOffset.Value;
        }

        await _serviceBusFactory.GetServiceBusSender(queueOrTopicName).SendMessageAsync(serviceBusMessage, cancellationToken);
    }
    
    /// <inheritdocs/>
    public async Task SendMessageAsync(string queueOrTopicName, string message, DateTimeOffset? dateTimeOffset = null, CancellationToken cancellationToken = default)
    {
        var serviceBusMessage = new ServiceBusMessage(message);
        if (dateTimeOffset != null)
        {
            serviceBusMessage.ScheduledEnqueueTime = dateTimeOffset.Value;
        }

        await _serviceBusFactory.GetServiceBusSender(queueOrTopicName).SendMessageAsync(serviceBusMessage, cancellationToken);
    }
}