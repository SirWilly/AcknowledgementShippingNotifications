using AcknowledgementShippingNotificationWatcher.Domain;

namespace AcknowledgementShippingNotificationWatcher.Parsers;

public class ShippingNotificationParser
{
    public AcknowledgementShippingNotificationDto Parse(string boxString)
    {
        var boxHeader = boxString.GetBoxHeader();
        var contents = boxString.GetBoxContents();

        return new AcknowledgementShippingNotificationDto
        {
            BoxHeader = boxHeader,
            Contents = contents
        };
    }
}