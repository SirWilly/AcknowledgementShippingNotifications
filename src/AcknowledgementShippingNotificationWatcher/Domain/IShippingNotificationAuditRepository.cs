namespace AcknowledgementShippingNotificationWatcher.Domain;

public interface IShippingNotificationAuditRepository
{
    void Add(AcknowledgementShippingNotification notification);
}