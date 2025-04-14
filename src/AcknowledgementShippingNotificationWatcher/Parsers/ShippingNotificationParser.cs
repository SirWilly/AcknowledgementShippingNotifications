using AcknowledgementShippingNotificationWatcher.Domain.NotificationInputs;
using Microsoft.Extensions.Logging;

namespace AcknowledgementShippingNotificationWatcher.Parsers;

public class ShippingNotificationParser(ILogger<ShippingNotificationParser> logger)
{
    public AcknowledgementShippingNotificationInput Parse(string boxString)
    {
        var boxHeader = ParseBoxHeader(boxString);
        var contents = ParseBoxContents(boxString);

        return new AcknowledgementShippingNotificationInput
        {
            BoxHeader = boxHeader,
            Contents = contents.ToList()
        };
    }
    
    public BoxHeaderInput? ParseBoxHeader(string boxString)
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

        return new BoxHeaderInput
        {
            SupplierId = boxHeaderLineParts[1],
            BoxId = boxHeaderLineParts[2]
        };
    }
    
    public IEnumerable<ProductInput?> ParseBoxContents(string boxString)
    {
        var lines = boxString.Split(Environment.NewLine);
        var productLines = lines.Where(line => line.Trim().StartsWith("LINE")).ToList();
        
        return productLines.Count == 0 
            ? [] 
            : productLines.Select(ParseProduct);
    }
    
    public ProductInput? ParseProduct(string productString)
    {
        var productLineParts = productString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (productLineParts.Length != 4)
        {
            logger.LogWarning("Unexpected product line part count: {ProductLinePartCount}", productLineParts.Length);
            return null;
        }
        
        var quantity = int.TryParse(productLineParts[3], out var result) ? result : (int?)null;

        return new ProductInput
        {
            PoNumber = productLineParts[1],
            Isbn = productLineParts[2],
            Quantity = quantity
        };
    }
}