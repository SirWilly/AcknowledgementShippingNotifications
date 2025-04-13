using AcknowledgementShippingNotificationWatcher.Domain;

namespace AcknowledgementShippingNotificationWatcher.Parsers;

public class ShippingNotificationParser
{
    public AcknowledgementShippingNotificationDto Parse(string boxString)
    {
        var boxHeader = ParseBoxHeader(boxString);
        var contents = boxString.GetBoxContents();

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
}