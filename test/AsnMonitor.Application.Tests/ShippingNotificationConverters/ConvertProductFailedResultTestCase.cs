using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;

namespace AsnMonitor.Application.Tests.ShippingNotificationConverters;

public class ConvertProductFailedResultTestCase
{
    public ProductInput? ProductInput { get; init; }
    public required string ExpectedReason { get; init; }
}