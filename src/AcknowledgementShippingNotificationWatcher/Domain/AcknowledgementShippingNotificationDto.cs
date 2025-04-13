namespace AcknowledgementShippingNotificationWatcher.Domain;

public record AcknowledgementShippingNotificationDto
{
    public BoxHeaderDto? BoxHeader { get; set; }
    public required IReadOnlyCollection<ProductDto?> Contents { get; set; }
}