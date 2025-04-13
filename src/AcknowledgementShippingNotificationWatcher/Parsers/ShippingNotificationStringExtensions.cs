using AcknowledgementShippingNotificationWatcher.Domain;

namespace AcknowledgementShippingNotificationWatcher.Parsers;

public static class ShippingNotificationStringExtensions
{
    public static BoxHeaderDto GetBoxHeader(this string boxString)
    {
        throw new NotImplementedException();
    }
    
    public static List<ProductDto> GetBoxContents(this string boxString)
    {
        throw new NotImplementedException();
    }
}