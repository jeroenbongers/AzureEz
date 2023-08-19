namespace AzureEz.ServiceBus.UnitTests.Extensions;

internal static class DateTimeOffsetExtensions
{
    internal static bool DateTimeOffsetIsWithinDelayRange(this DateTimeOffset targetDateTimeOffset, DateTimeOffset sourceDateTimeOffset)
    {
        return targetDateTimeOffset < sourceDateTimeOffset.AddSeconds(1) &&
               targetDateTimeOffset > sourceDateTimeOffset.AddSeconds(-1);
    }
}