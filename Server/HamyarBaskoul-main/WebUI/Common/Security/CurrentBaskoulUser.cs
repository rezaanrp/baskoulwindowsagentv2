using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace WebUI.Common.Security;

public sealed class CurrentBaskoulUser(IHttpContextAccessor accessor, UserManager<AppUser> userManager) : ICurrentBaskoulUser
{
    public async Task<CurrentBaskoulScope> GetScopeAsync(CancellationToken cancellationToken = default)
    {
        var principal = accessor.HttpContext?.User ?? throw new UnauthorizedAccessException();
        var user = await userManager.GetUserAsync(principal) ?? throw new UnauthorizedAccessException();
        if (string.IsNullOrWhiteSpace(user.CodMarkaz) || !user.SelectedSiteId.HasValue)
            throw new InvalidOperationException("مرکز یا سایت فعال برای کاربر مشخص نشده است.");
        return new CurrentBaskoulScope(user.Id, user.CodMarkaz, user.SelectedSiteId.Value);
    }
}
