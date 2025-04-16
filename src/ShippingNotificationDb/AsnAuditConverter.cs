using AcknowledgementShippingNotificationWatcher.Domain;
using ShippingNotificationDb.Models;

namespace ShippingNotificationDb;

public static class AsnAuditConverter
{
    public static Box Convert(AcknowledgementShippingNotification acknowledgementShippingNotification)
    {
        return new Box
        {
            BoxId = acknowledgementShippingNotification.BoxId,
            SupplierId = acknowledgementShippingNotification.SupplierId,
            Products = acknowledgementShippingNotification.Contents.Select(Convert).ToList()
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