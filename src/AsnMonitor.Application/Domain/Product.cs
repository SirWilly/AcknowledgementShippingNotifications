namespace AcknowledgementShippingNotificationWatcher.Domain;

public class Product
{
    public required string PoNumber { get; init; }
    public required string Isbn { get; init; }
    public int Quantity { get; init; }
}