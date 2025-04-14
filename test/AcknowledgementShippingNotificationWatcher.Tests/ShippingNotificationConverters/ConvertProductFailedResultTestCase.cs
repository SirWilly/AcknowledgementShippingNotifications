using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;

namespace AcknowledgementShippingNotificationWatcher.Tests.ShippingNotificationConverters;

public class ConvertProductFailedResultTestCase
{
    public ProductInput? ProductInput { get; init; }
    public required string ExpectedReason { get; init; }
}