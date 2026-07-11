namespace Application.Common.Interfaces;

public sealed record CurrentBaskoulScope(string UserId, string CodeMarkaz, int SiteId);

public interface ICurrentBaskoulUser
{
    Task<CurrentBaskoulScope> GetScopeAsync(CancellationToken cancellationToken = default);
}
