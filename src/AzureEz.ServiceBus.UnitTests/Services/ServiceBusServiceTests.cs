using AutoFixture.Xunit2;
using Azure.Messaging.ServiceBus;
using AzureEz.ServiceBus.Infrastructure.Interfaces;
using AzureEz.ServiceBus.Services;
using AzureEz.ServiceBus.UnitTests.Attributes;
using AzureEz.ServiceBus.UnitTests.Dtos;
using AzureEz.ServiceBus.UnitTests.Extensions;

namespace AzureEz.ServiceBus.UnitTests.Services;

public class ServiceBusServiceTests
{
    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_SbMessageWithoutDelay_SendsMessageWithoutDelay(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        ServiceBusMessage sbMessage)
    {
        await sut.SendMessageAsync(queueOrTopicName, sbMessage);

        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(sbMessage, CancellationToken.None));
    }

    #region JsonObjectMessageBody

    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_JsonObjectMessageBody_WithoutDelay_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        TestMessageDto messageBody)
    {
        await sut.SendMessageAsync(queueOrTopicName, messageBody, delay: null);

        var expectedSbMessageBinaryData = BinaryData.FromObjectAsJson(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString()), CancellationToken.None));
    }

    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_JsonObjectMessageBody_WithDelay_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        TestMessageDto messageBody,
        short minutesDelay)
    {
        var startDateTimeOffset = DateTimeOffset.UtcNow;
        var delay = TimeSpan.FromMinutes(minutesDelay);
        await sut.SendMessageAsync(queueOrTopicName, messageBody, delay: delay);

        var expectedSbMessageBinaryData = BinaryData.FromObjectAsJson(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y =>
            y.Body.ToString() == expectedSbMessageBinaryData.ToString() && 
            y.ScheduledEnqueueTime.DateTimeOffsetIsWithinDelayRange(startDateTimeOffset.Add(delay))), CancellationToken.None));
    }

    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_JsonObjectMessageBody_WithoutDateTimeOffset_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        TestMessageDto messageBody)
    {
        await sut.SendMessageAsync(queueOrTopicName, messageBody, dateTimeOffset: null);

        var expectedSbMessageBinaryData = BinaryData.FromObjectAsJson(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString()), CancellationToken.None));
    }

    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_JsonObjectMessageBody_WithDateTimeOffset_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        TestMessageDto messageBody,
        short minutesDelay)
    {
        var startDateTimeOffset = DateTimeOffset.UtcNow;
        var enqueueDateTimeOffset = startDateTimeOffset.AddMinutes(minutesDelay);
        await sut.SendMessageAsync(queueOrTopicName, messageBody, dateTimeOffset: enqueueDateTimeOffset);

        var expectedSbMessageBinaryData = BinaryData.FromObjectAsJson(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y =>
            y.Body.ToString() == expectedSbMessageBinaryData.ToString() &&
            y.ScheduledEnqueueTime.DateTimeOffsetIsWithinDelayRange(enqueueDateTimeOffset)), CancellationToken.None));
    }

    #endregion

    #region ByteArrayMessageBody

    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_ByteArrayMessageBody_WithoutDelay_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        byte[] messageBody)
    {
        await sut.SendMessageAsync(queueOrTopicName, messageBody, delay: null);

        var expectedSbMessageBinaryData = BinaryData.FromBytes(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString()), CancellationToken.None));
    }
    
    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_ByteArrayMessageBody_WithDelay_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        byte[] messageBody,
        short minutesDelay)
    {
        var startingDateTimeOffset = DateTimeOffset.UtcNow;
        var delay = TimeSpan.FromMinutes(minutesDelay);
        await sut.SendMessageAsync(queueOrTopicName, messageBody, delay: delay);

        var expectedSbMessageBinaryData = BinaryData.FromBytes(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString() && 
            y.ScheduledEnqueueTime.DateTimeOffsetIsWithinDelayRange(startingDateTimeOffset.Add(delay))), CancellationToken.None));
    }
    
    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_ByteArrayMessageBody_WithoutDateTimeOffset_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        byte[] messageBody)
    {
        await sut.SendMessageAsync(queueOrTopicName, messageBody, dateTimeOffset: null);

        var expectedSbMessageBinaryData = BinaryData.FromBytes(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString()), CancellationToken.None));
    }
    
    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_ByteArrayMessageBody_WithDateTimeOffset_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        byte[] messageBody,
        short minutesDelay)
    {
        var startingDateTimeOffset = DateTimeOffset.UtcNow;
        var enqueuedDateTimeOffset = startingDateTimeOffset.AddMinutes(minutesDelay);
        await sut.SendMessageAsync(queueOrTopicName, messageBody, dateTimeOffset: enqueuedDateTimeOffset);

        var expectedSbMessageBinaryData = BinaryData.FromBytes(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString() && 
            y.ScheduledEnqueueTime.DateTimeOffsetIsWithinDelayRange(enqueuedDateTimeOffset)), CancellationToken.None));
    }

    #endregion

    #region StringMessageBody

    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_StringMessageBody_WithoutDelay_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        string messageBody)
    {
        await sut.SendMessageAsync(queueOrTopicName, messageBody, delay: null);

        var expectedSbMessageBinaryData = BinaryData.FromString(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x =>
            x.SendMessageAsync(It.Is<ServiceBusMessage>(y => y.Body.ToString() == expectedSbMessageBinaryData.ToString()), CancellationToken.None));
    }
    
    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_StringMessageBody_WithDelay_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        string messageBody,
        short minutesDelay)
    {
        var startingDateTimeOffset = DateTimeOffset.UtcNow;
        var delay = TimeSpan.FromMinutes(minutesDelay);
        await sut.SendMessageAsync(queueOrTopicName, messageBody, delay: delay);

        var expectedSbMessageBinaryData = BinaryData.FromString(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString() && 
            y.ScheduledEnqueueTime.DateTimeOffsetIsWithinDelayRange(startingDateTimeOffset.Add(delay))), CancellationToken.None));
    }

    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_StringMessageBody_WithoutDateTimeOffset_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        string messageBody)
    {
        await sut.SendMessageAsync(queueOrTopicName, messageBody, dateTimeOffset: null);

        var expectedSbMessageBinaryData = BinaryData.FromString(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x =>
            x.SendMessageAsync(It.Is<ServiceBusMessage>(y => y.Body.ToString() == expectedSbMessageBinaryData.ToString()), CancellationToken.None));
    }
    
    [Theory, AutoMoqData]
    internal async Task SendMessageAsync_StringMessageBody_WithDateTimeOffset_SendsMessageWithExpectedMessageBody(
        [Frozen] Mock<IServiceBusFactory> serviceBusFactoryMock,
        [Frozen] Mock<ServiceBusSender> serviceBusSenderMock,
        ServiceBusService sut,
        string queueOrTopicName,
        string messageBody,
        short minutesDelay)
    {
        var startingDateTimeOffset = DateTimeOffset.UtcNow;
        var enqueuedDateTimeOffset = startingDateTimeOffset.AddMinutes(minutesDelay);
        await sut.SendMessageAsync(queueOrTopicName, messageBody, dateTimeOffset: enqueuedDateTimeOffset);

        var expectedSbMessageBinaryData = BinaryData.FromString(messageBody);
        serviceBusFactoryMock.Verify(x => x.GetServiceBusSender(queueOrTopicName));
        serviceBusSenderMock.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(y => 
            y.Body.ToString() == expectedSbMessageBinaryData.ToString() && 
            y.ScheduledEnqueueTime.DateTimeOffsetIsWithinDelayRange(enqueuedDateTimeOffset)), CancellationToken.None));
    }

    #endregion
}