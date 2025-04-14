using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationConverters;

public class ConvertAcknowledgementShippingNotificationFailedResultTestCase
{
    public required AcknowledgementShippingNotificationInput Input { get; init; }
    public required string ExpectedReason { get; init; }
}