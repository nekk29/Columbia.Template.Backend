using System.Security.Claims;
using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class GenerateTokenCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator,
        GenerateTokenCommandValidator validator,
        UserManager<Entity.ApplicationUser> userManager
    ) : CommandHandlerBase<GenerateTokenCommand, ClaimsPrincipal>(unitOfWork, mapper, mediator, validator)
    {
        public override async Task<ResponseDto<ClaimsPrincipal>> HandleCommand(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = request.ApplicationUser;

            var identity = new ClaimsIdentity(
               OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
               OpenIddictConstants.Claims.Name,
               OpenIddictConstants.Claims.Role
            );

            identity.AddClaim(OpenIddictConstants.Claims.Subject, user.Id, OpenIddictConstants.Destinations.AccessToken);
            identity.AddClaim(OpenIddictConstants.Claims.Email, user.Email!, OpenIddictConstants.Destinations.AccessToken);
            identity.AddClaim(OpenIddictConstants.Claims.JwtId, Guid.NewGuid().ToString(), OpenIddictConstants.Destinations.AccessToken);
            identity.AddClaim(OpenIddictConstants.Claims.Name, $"{user!.FirstName} {user!.LastName}", OpenIddictConstants.Destinations.IssuedToken);
            identity.AddClaim(OpenIddictConstants.Claims.Username, user!.UserName!, OpenIddictConstants.Destinations.IssuedToken);
            identity.AddClaim(OpenIddictConstants.Claims.Website, request!.ApplicationCode!, OpenIddictConstants.Destinations.IssuedToken);

            foreach (var role in await userManager.GetRolesAsync(user))
                identity.AddClaim(OpenIddictConstants.Claims.Role, role, OpenIddictConstants.Destinations.AccessToken);

            var principal = new ClaimsPrincipal(identity);

            principal.SetScopes(request.Scopes);

            return new ResponseDto<ClaimsPrincipal>(principal);
        }
    }
}
