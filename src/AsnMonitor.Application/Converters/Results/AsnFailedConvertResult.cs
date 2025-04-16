namespace AsnMonitor.Application.Converters.Results;

public class AsnFailedConvertResult : IAsnConvertResult
{
    public required string Reason { get; init; }
    public List<ProductConvertFailureResult> FailedProductConvertResults { get; set; } = [];
}