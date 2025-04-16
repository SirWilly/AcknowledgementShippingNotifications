namespace AsnMonitor.Application.Converters.Results;

public class ProductConvertFailureResult : IProductConvertResult
{
    public required string Reason { get; init; }
}