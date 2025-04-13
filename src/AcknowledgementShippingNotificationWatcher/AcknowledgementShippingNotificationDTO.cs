namespace AcknowledgementShippingNotificationWatcher;

public record AcknowledgementShippingNotificationDto
{
    public string? BoxId { get; set; }
    public string? SupplierId { get; set; }
    public IReadOnlyCollection<ProductDto>? Contents { get; set; }
}

public record ProductDto
{
    public string? PoNumber { get; set; }
    public string? Isbn { get; set; }
    public int? Quantity { get; set; }
}