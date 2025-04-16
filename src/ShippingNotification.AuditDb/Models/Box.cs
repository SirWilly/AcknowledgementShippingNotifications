using System.ComponentModel.DataAnnotations;

namespace ShippingNotification.AuditDb.Models;

public class Box
{
    [Key]
    public string BoxId { get; set; }
    public string SupplierId { get; set; }
    
    public IReadOnlyCollection<ProductLine> Products { get; set; }
}