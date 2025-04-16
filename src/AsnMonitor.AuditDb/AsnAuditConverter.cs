using AsnMonitor.Application;
using AsnMonitor.AuditDb.Models;

namespace AsnMonitor.AuditDb;

public static class AsnAuditConverter
{
    public static Box Convert(Asn asn)
    {
        return new Box
        {
            BoxId = asn.BoxId,
            SupplierId = asn.SupplierId,
            Products = asn.Contents.Select(Convert).ToList()
        };
    }

    public static ProductLine Convert(Product product)
    {
        return new ProductLine
        {
            Id = Guid.NewGuid(),
            PoNumber = product.PoNumber,
            Isbn = product.Isbn,
            Quantity = product.Quantity,
        };
    }
}