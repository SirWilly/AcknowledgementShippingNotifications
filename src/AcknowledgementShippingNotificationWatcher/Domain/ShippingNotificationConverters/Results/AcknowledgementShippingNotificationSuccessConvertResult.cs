namespace AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;

public class AcknowledgementShippingNotificationSuccessConvertResult : IAcknowledgementShippingNotificationConvertResult
{
    public required AcknowledgementShippingNotification AcknowledgementShippingNotification { get; init; }
}