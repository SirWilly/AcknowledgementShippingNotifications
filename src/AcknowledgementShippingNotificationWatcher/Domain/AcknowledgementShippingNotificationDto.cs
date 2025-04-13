namespace AcknowledgementShippingNotificationWatcher.Domain;

public record AcknowledgementShippingNotificationDto
{
    public BoxHeaderDto BoxHeader { get; set; }
    public IReadOnlyCollection<ProductDto>? Contents { get; set; }
}