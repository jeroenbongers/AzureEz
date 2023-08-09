using Azure.Messaging.ServiceBus;

namespace AzureEz.ServiceBus.Infrastructure.Interfaces;

public interface IServiceBusFactory
{
    public ServiceBusSender GetServiceBusSender(string queueOrTopicName);
    public ServiceBusReceiver GetServiceBusReceiver(string queueName);
    public ServiceBusReceiver GetServiceBusReceiver(string topicName, string subscriptionName);
}