namespace AsnMonitor.Application.Converters.Results;

public class AsnSuccessConvertResult : IAsnConvertResult
{
    public required Asn Asn { get; init; }
}