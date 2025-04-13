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
            Contents = contents.ToList()
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
    
    public IEnumerable<ProductDto?> ParseBoxContents(string boxString)
    {
        var lines = boxString.Split(Environment.NewLine);
        var productLines = lines.Where(line => line.Trim().StartsWith("LINE")).ToList();
        
        return productLines.Count == 0 
            ? [] 
            : productLines.Select(ParseProduct);
    }
    
    public ProductDto? ParseProduct(string productString)
    {
        var productLineParts = productString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (productLineParts.Length != 4)
        {
            logger.LogWarning("Unexpected product line part count: {ProductLinePartCount}", productLineParts.Length);
            return null;
        }
        
        var quantity = int.TryParse(productLineParts[3], out var result) ? result : (int?)null;

        return new ProductDto
        {
            PoNumber = productLineParts[1],
            Isbn = productLineParts[2],
            Quantity = quantity
        };
    }
}