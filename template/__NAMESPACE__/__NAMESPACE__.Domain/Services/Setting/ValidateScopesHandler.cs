using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Server;
using static OpenIddict.Server.OpenIddictServerEvents;

namespace __NAMESPACE__.Domain.Services.Setting
{
    public class ValidateScopesHandler(
        IOpenIddictApplicationManager openIddictApplicationManager
    ) : IOpenIddictServerHandler<ValidateTokenRequestContext>
    {
        public async ValueTask HandleAsync(ValidateTokenRequestContext context)
        {
            var client = await openIddictApplicationManager.FindByClientIdAsync(context.ClientId!);
            if (client is OpenIddictEntityFrameworkCoreApplication application)
            {
                var permissions = await openIddictApplicationManager.GetPermissionsAsync(application);

                var scopes = permissions
                    .Where(p => p.StartsWith(OpenIddictConstants.Permissions.Prefixes.Scope))
                    .Select(p => p[OpenIddictConstants.Permissions.Prefixes.Scope.Length..])
                    .ToList();

                foreach (var scope in scopes)
                    context.Options.Scopes.Add(scope);
            }
        }
    }
}
