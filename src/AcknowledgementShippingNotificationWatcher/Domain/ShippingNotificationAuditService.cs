using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters;
using AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;

namespace AcknowledgementShippingNotificationWatcher.Domain;

public class ShippingNotificationAuditService(
    IAcknowledgementShippingNotificationConverter acknowledgementShippingNotificationConverter,
    IShippingNotificationAuditRepository repository)
{
    public void AuditNotification(AcknowledgementShippingNotificationInput notificationDto)
    {
        var result = acknowledgementShippingNotificationConverter.Convert(notificationDto);
        switch (result)
        {
            case AcknowledgementShippingNotificationFailedConvertResult failedResult:
                // It is likely that here we would not want to throw but instead log an error or make it known to a user
                // that we encountered a corrupt record.
                throw new FormatException($"Failed to convert shipping notification input. Reason: {failedResult.Reason}");
            case AcknowledgementShippingNotificationSuccessConvertResult successResult:
                repository.Save(successResult.AcknowledgementShippingNotification);
                break;
            default:
                throw new Exception("Unexpected result from AcknowledgementShippingNotificationConverter");
        }
    }
}