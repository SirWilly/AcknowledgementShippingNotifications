namespace AcknowledgementShippingNotificationWatcher.Domain;

public class AcknowledgementShippingNotification
{
    public required string BoxId { get; set; }
    public required string SupplierId { get; set; }
    
    public required IReadOnlyCollection<Product> Contents { get; set; }
}