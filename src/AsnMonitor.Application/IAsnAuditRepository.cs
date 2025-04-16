namespace AsnMonitor.Application;

public interface IAsnAuditRepository
{
    void Add(Asn notification);
}