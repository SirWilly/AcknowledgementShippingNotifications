namespace AcknowledgementShippingNotificationWatcher.Domain.ShippingNotificationConverters.Results;

public class ProductConvertSuccessResult : IProductConvertResult
{
    public required Product Product { get; init; }
}