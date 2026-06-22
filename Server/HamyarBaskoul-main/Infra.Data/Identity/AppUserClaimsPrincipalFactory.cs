using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Models; // اطمینان حاصل کن که namespace درست باشه

public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
{
	public AppUserClaimsPrincipalFactory(
		UserManager<AppUser> userManager,
		RoleManager<IdentityRole> roleManager,
		IOptions<IdentityOptions> optionsAccessor)
		: base(userManager, roleManager, optionsAccessor)
	{
	}

	protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
	{
		var identity = await base.GenerateClaimsAsync(user);
		identity.AddClaim(new Claim("Name", user.Name ?? ""));
		identity.AddClaim(new Claim("Family", user.Family ?? ""));
		return identity;
	}
}

