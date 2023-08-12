using Azure.Core.Amqp;
using Azure.Messaging.ServiceBus;
using AzureEz.ServiceBus.Infrastructure.Interfaces;
using AzureEz.ServiceBus.Services;
using AzureEz.ServiceBus.UnitTests.Attributes;

namespace AzureEz.ServiceBus.UnitTests.Services;

public class ServiceBusServiceTests
{
    [Theory, AutoNSubstituteData]
    public async Task SendMessageAsync_SbMessageWithoutDelay_SendsMessageWithoutDelay(
        [Substitute] IServiceBusFactory serviceBusFactorySubstitute,
        [Substitute] ServiceBusSender serviceBusSenderSubstitute,
        string queueOrTopicName, 
        ServiceBusMessage sbMessage)
    {
        serviceBusFactorySubstitute.GetServiceBusSender(queueOrTopicName).Returns(serviceBusSenderSubstitute);

        var sut = new ServiceBusService(serviceBusFactorySubstitute);
        await sut.SendMessageAsync(queueOrTopicName, sbMessage);

        await serviceBusSenderSubstitute.Received().SendMessageAsync(sbMessage);
    }
    
    [Theory, AutoNSubstituteData]
    public async Task SendMessageAsync_StringMessageWithoutDelay_SendsMessageWithoutDelay(
        [Substitute] IServiceBusFactory serviceBusFactorySubstitute,
        [Substitute] ServiceBusSender serviceBusSenderSubstitute,
        string queueOrTopicName, 
        string sbMessageBody)
    {
        serviceBusFactorySubstitute.GetServiceBusSender(queueOrTopicName).Returns(serviceBusSenderSubstitute);

        var sut = new ServiceBusService(serviceBusFactorySubstitute);
        await sut.SendMessageAsync(queueOrTopicName, sbMessageBody, delay: null);

        await serviceBusSenderSubstitute.Received()
            .SendMessageAsync(Arg.Any<ServiceBusMessage>());
    }
}