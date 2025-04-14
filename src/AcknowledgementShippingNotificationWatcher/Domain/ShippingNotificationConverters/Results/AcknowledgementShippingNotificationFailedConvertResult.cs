namespace AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;

public class AcknowledgementShippingNotificationFailedConvertResult : IAcknowledgementShippingNotificationConvertResult
{
    public required string Reason { get; init; }
    public List<ProductConvertFailureResult> FailedProductConvertResults { get; set; } = [];
}