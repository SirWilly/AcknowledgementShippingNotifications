namespace AcknowledgementShippingNotificationWatcher.Domain;

public record ProductDto
{
    public string? PoNumber { get; set; }
    public string? Isbn { get; set; }
    public int? Quantity { get; set; }
}