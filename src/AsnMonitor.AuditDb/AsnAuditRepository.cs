using AsnMonitor.Application;

namespace AsnMonitor.AuditDb;

public class AsnAuditRepository : IAsnAuditRepository
{
    private readonly AsnAuditContext _asnAuditContext = new();
    
    public void Add(Asn notification)
    {
        var box = AsnAuditConverter.Convert(notification);
        
        _asnAuditContext.Add(box);
        _asnAuditContext.SaveChanges();
    }
}