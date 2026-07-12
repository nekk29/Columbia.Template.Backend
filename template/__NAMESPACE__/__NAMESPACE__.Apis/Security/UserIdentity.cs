using __NAMESPACE__.Repository.Abstractions.Security;
using Microsoft.AspNetCore.Identity;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace __NAMESPACE__.Apis.Security
{
    public class UserIdentity(
        IHttpContextAccessor httpContextAccessor,
        UserManager<Entity.ApplicationUser> userManager
    ) : IUserIdentity
    {

        public Guid GetSubject()
        {
            var principal =httpContextAccessor.HttpContext!.User;
            var subClaim = principal!.Claims.FirstOrDefault(x => x.Type == Claims.Subject)?.Value;
            var subParsed = Guid.TryParse(subClaim?.Replace("\"", string.Empty), out Guid userId);
            
            return subParsed ? userId : default;
        }

        public string GetUserName()
        {
            var subject = GetSubject().ToString();
            var user = userManager.FindByIdAsync(subject).Result!;
            return user?.UserName!;
        }
    }
}
