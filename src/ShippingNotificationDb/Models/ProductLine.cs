namespace ShippingNotificationDb.Models;

public class ProductLine
{
    public Guid Id { get; set; }
    public string PoNumber { get; set; }
    public string Isbn { get; set; }
    public int Quantity { get; set; }
}