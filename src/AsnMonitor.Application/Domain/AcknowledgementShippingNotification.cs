namespace AcknowledgementShippingNotificationWatcher.Domain;

public class AcknowledgementShippingNotification
{
    public required string BoxId { get; init; }
    public required string SupplierId { get; init; }
    
    public required IReadOnlyCollection<Product> Contents { get; init; }
}