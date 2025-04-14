namespace AcknowledgementShippingNotificationWatcher.Domain;

public class Product
{
    public required string PoNumber { get; set; }
    public required string Isbn { get; set; }
    public int Quantity { get; set; }
}