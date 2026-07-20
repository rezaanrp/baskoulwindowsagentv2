namespace Application.Features.BaskoulV2;

public sealed record LookupItemDto(long Id, string Title);
public sealed record WeighbridgeDto(int Id, string Name, string ScaleCode, int? Type, float Weight);

public sealed record BaskoulFormDto(
    IReadOnlyList<LookupItemDto> Drivers,
    IReadOnlyList<WeighbridgeDto> Weighbridges,
    string CodeMarkaz,
    int SiteId);

public sealed record BargeDto(
    long Id,
    long? ReceiptNumber,
    string Plate,
    long? DriverId,
    string DriverName,
    float? EntryWeight,
    float? ExitWeight,
    float? NetWeight,
    long? WeighbridgeId,
    string BargeType,
    string Status,
    string? Description);

public sealed record PagedBargesDto(IReadOnlyList<BargeDto> Items, int Page, int PageSize, int TotalCount);

public sealed record WeightCommandResult(long Id, long? ReceiptNumber, string Status, string Message);

public sealed class BaskoulConflictException(string message) : Exception(message);
public sealed class BaskoulNotFoundException(string message) : Exception(message);
