namespace AsnMonitor.Application.NotificationInputs;

public record AsnInput
{
    public BoxHeaderInput? BoxHeader { get; init; }
    public required IReadOnlyCollection<ProductInput?> Contents { get; init; }
}