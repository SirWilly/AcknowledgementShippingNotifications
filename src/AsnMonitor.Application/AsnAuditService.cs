using AsnMonitor.Application.Converters;
using AsnMonitor.Application.Converters.Results;
using AsnMonitor.Application.NotificationInputs;

namespace AsnMonitor.Application;

public class AsnAuditService(
    IAsnConverter asnConverter,
    IAsnAuditRepository repository)
{
    public void AuditNotification(AsnInput notificationDto)
    {
        var result = asnConverter.Convert(notificationDto);
        switch (result)
        {
            case AsnFailedConvertResult failedResult:
                // It is likely that here we would not want to throw but instead log an error or make it known to a user
                // that we encountered a corrupt record.
                throw new FormatException($"Failed to convert shipping notification input. Reason: {failedResult.Reason}");
            case AsnSuccessConvertResult successResult:
                repository.Add(successResult.Asn);
                break;
            default:
                throw new Exception("Unexpected result from AsnConverter");
        }
    }
}