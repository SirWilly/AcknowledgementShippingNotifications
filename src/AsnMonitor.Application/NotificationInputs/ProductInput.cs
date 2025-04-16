namespace AsnMonitor.Application.NotificationInputs;

public record ProductInput
{
    public string? PoNumber { get; init; }
    public string? Isbn { get; init; }
    public int? Quantity { get; init; }
}