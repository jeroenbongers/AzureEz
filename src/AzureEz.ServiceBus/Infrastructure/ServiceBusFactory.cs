using System.Collections.Concurrent;
using AzureEz.ServiceBus.Infrastructure.Interfaces;

namespace AzureEz.ServiceBus.Infrastructure;

public class ServiceBusFactory : IServiceBusFactory
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ConcurrentDictionary<string, ServiceBusSender> _serviceBusSenders = new ();
    private readonly ConcurrentDictionary<string, ServiceBusReceiver> _serviceBusReceivers = new ();

    public ServiceBusFactory(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public ServiceBusSender GetServiceBusSender(string queueOrTopicName)
    {
        return _serviceBusSenders.GetOrAdd(queueOrTopicName, queueOrTopicKey => _serviceBusClient.CreateSender(queueOrTopicKey));
    }
    
    public ServiceBusReceiver GetServiceBusReceiver(string queueName)
    {
        return _serviceBusReceivers.GetOrAdd(queueName, queueNameKey => _serviceBusClient.CreateReceiver(queueNameKey));
    }
    
    public ServiceBusReceiver GetServiceBusReceiver(string topicName, string subscriptionName)
    {
        return _serviceBusReceivers.GetOrAdd($"{topicName}/{subscriptionName}", _ => _serviceBusClient.CreateReceiver(topicName, subscriptionName));
    }
}