using AcknowledgementShippingNotificationWatcher.Domain;
using Microsoft.Extensions.Logging;

namespace AcknowledgementShippingNotificationWatcher.Parsers;

public class ShippingNotificationParser(ILogger<ShippingNotificationParser> logger)
{
    public AcknowledgementShippingNotificationDto Parse(string boxString)
    {
        var boxHeader = ParseBoxHeader(boxString);
        var contents = ParseBoxContents(boxString);

        return new AcknowledgementShippingNotificationDto
        {
            BoxHeader = boxHeader,
            Contents = contents
        };
    }
    
    public BoxHeaderDto? ParseBoxHeader(string boxString)
    {
        var lines = boxString.Split(Environment.NewLine);
        
        var boxHeaderLine = lines.SingleOrDefault(line => line.Trim().StartsWith("HDR"));
        if (boxHeaderLine is null)
        {
            logger.LogWarning($"Box header not found");
            return null;
        }
        
        var boxHeaderLineParts = boxHeaderLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (boxHeaderLineParts.Length != 3)
        {
            logger.LogWarning("Unexpected box header line part count: {BoxHeaderLinePartCount}"
                ,boxHeaderLineParts.Length);
            return null;
        }

        return new BoxHeaderDto
        {
            SupplierId = boxHeaderLineParts[1],
            BoxId = boxHeaderLineParts[2]
        };
    }
    
    public static List<ProductDto> ParseBoxContents(string boxString)
    {
        throw new NotImplementedException();
    }
    
    
}