namespace AsnMonitor.Application.Converters.Results;

public class ProductConvertSuccessResult : IProductConvertResult
{
    public required Product Product { get; init; }
}