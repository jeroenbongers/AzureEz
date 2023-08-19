using AutoFixture;
using AutoFixture.Xunit2;
using Azure.Messaging.ServiceBus;
using AzureEz.ServiceBus.Infrastructure.Interfaces;

namespace AzureEz.ServiceBus.UnitTests.Attributes;

public class AutoMoqData : AutoDataAttribute
{
    public AutoMoqData() : base(GetCustomFixture){}

    private static IFixture GetCustomFixture()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());

        var serviceBusSenderMock = new Mock<ServiceBusSender>();
        serviceBusSenderMock.Setup(x => x.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        fixture.Register(() => serviceBusSenderMock);

        var serviceBusFactoryMock = new Mock<IServiceBusFactory>();
        serviceBusFactoryMock.Setup(x => x.GetServiceBusSender(It.IsAny<string>())).Returns(serviceBusSenderMock.Object);
        fixture.Register(() => serviceBusFactoryMock);
        
        return fixture;
    }
}