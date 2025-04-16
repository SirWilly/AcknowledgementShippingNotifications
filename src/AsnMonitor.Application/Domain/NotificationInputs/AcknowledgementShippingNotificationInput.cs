namespace AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;

public record AcknowledgementShippingNotificationInput
{
    public BoxHeaderInput? BoxHeader { get; set; }
    public required IReadOnlyCollection<ProductInput?> Contents { get; set; }
}