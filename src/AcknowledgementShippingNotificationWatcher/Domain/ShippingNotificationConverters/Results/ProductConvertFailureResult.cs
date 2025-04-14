namespace AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;

public class ProductConvertFailureResult : IProductConvertResult
{
    public required string Reason { get; init; }
}