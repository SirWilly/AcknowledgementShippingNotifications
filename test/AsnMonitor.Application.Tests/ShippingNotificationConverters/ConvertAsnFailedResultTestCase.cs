using AsnMonitor.Application.NotificationInputs;

namespace AsnMonitor.Application.Tests.ShippingNotificationConverters;

public class ConvertAsnFailedResultTestCase
{
    public required AsnInput Input { get; init; }
    public required string ExpectedReason { get; init; }
}