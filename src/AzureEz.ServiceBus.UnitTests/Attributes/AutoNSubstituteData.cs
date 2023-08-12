using AutoFixture;
using AutoFixture.Xunit2;

namespace AzureEz.ServiceBus.UnitTests.Attributes;

public class AutoNSubstituteData : AutoDataAttribute
{
    public AutoNSubstituteData() : base(GetCustomFixture){}

    private static IFixture GetCustomFixture()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoNSubstituteCustomization());
        return fixture;
    }
}