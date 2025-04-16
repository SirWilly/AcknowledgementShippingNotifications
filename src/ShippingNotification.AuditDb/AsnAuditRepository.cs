using AcknowledgementShippingNotificationWatcher.Domain;

namespace ShippingNotification.AuditDb;

public class AsnAuditRepository : IShippingNotificationAuditRepository
{
    private readonly AsnAuditContext _asnAuditContext = new();
    
    public void Add(AcknowledgementShippingNotification notification)
    {
        var box = AsnAuditConverter.Convert(notification);
        
        _asnAuditContext.Add(box);
        _asnAuditContext.SaveChanges();
    }
}