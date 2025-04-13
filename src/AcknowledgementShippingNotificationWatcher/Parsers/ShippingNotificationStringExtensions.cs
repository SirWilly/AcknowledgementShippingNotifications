using AcknowledgementShippingNotificationWatcher.Domain;

namespace AcknowledgementShippingNotificationWatcher.Parsers;

public static class ShippingNotificationStringExtensions
{
    // Depending on business requirements we could do something else instead of throwing an error if the string is not
    // in expected format.
    public static BoxHeaderDto? GetBoxHeader(this string boxString)
    {
        var lines = boxString.Split(Environment.NewLine);
        
        var boxHeaderLine = lines.SingleOrDefault(line => line.Trim().StartsWith("HDR"));
        if (boxHeaderLine is null)
        {
            return null;
        }
        
        var boxHeaderLineParts = boxHeaderLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (boxHeaderLineParts.Length != 3)
        {
            throw new Exception($"Invalid box header line: {boxString}");
        }

        return new BoxHeaderDto
        {
            SupplierId = boxHeaderLineParts[1],
            BoxId = boxHeaderLineParts[2]
        };
    }
    
    public static List<ProductDto> GetBoxContents(this string boxString)
    {
        throw new NotImplementedException();
    }
}