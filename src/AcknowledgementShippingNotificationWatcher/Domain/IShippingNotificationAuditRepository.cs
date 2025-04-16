namespace AcknowledgementShippingNotificationWatcher.Domain;

public interface IShippingNotificationAuditRepository
{
    void Save(AcknowledgementShippingNotification notification);
}