using System.Text.Json;
using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Client;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

namespace __NAMESPACE__.Domain.Commands.Client
{
    public class CreateOrUpdateClientCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        CreateOrUpdateClientCommandValidator validator,
        IOpenIddictApplicationManager openIddictApplicationManager
    ) : CommandHandlerBase<CreateOrUpdateClientCommand, CreateOrUpdateClientResultDto>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto<CreateOrUpdateClientResultDto>> HandleCommand(CreateOrUpdateClientCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<CreateOrUpdateClientResultDto>();

            try
            {
                var clientId = await CreateOrUpdateClient(request, cancellationToken);
                response.UpdateData(new CreateOrUpdateClientResultDto { ClientId = clientId });
            }
            catch (Exception ex)
            {
                response.AddErrorResult(Resources.Client.CreateOrUpdateClientErrorClient, ex);
            }

            return response;
        }

        private async Task<string> CreateOrUpdateClient(CreateOrUpdateClientCommand request, CancellationToken cancellationToken)
        {
            var createDto = request.CreateDto;

            var applicationCode = createDto.ApplicationCode;
            var applicationCodeOld = request.CreateDto.OldApplicationCode;

            var clientId = $"{applicationCode}".ToLower();
            var clientIdOld = !string.IsNullOrEmpty(applicationCodeOld) ? $"{applicationCodeOld}".ToLower() : clientId;

            var clientApiScope = $"{clientId}.apis";
            var clientDisplayName = createDto.ApplicationName ?? JsonNamingPolicy.CamelCase.ConvertName(applicationCode);

            var applicationUri = request.CreateDto.ApplicationUri;
            var clientUri = applicationUri?.EndsWith('/') == true ? applicationUri[0..^1] : applicationUri;

            var signinRedirectUri = !string.IsNullOrEmpty(createDto.SigninRedirectUri) ? createDto.SigninRedirectUri : "/auth/signin";
            var refreshRedirectUri = !string.IsNullOrEmpty(createDto.RefreshRedirectUri) ? createDto.RefreshRedirectUri : "/silent-refresh.html";
            var postLogoutRedirectUri = !string.IsNullOrEmpty(createDto.PostLogoutRedirectUri) ? createDto.PostLogoutRedirectUri : "/auth/signout";

            signinRedirectUri = signinRedirectUri?.StartsWith('/') == true ? signinRedirectUri[1..] : signinRedirectUri;
            refreshRedirectUri = refreshRedirectUri?.StartsWith('/') == true ? refreshRedirectUri[1..] : refreshRedirectUri;
            postLogoutRedirectUri = postLogoutRedirectUri?.StartsWith('/') == true ? postLogoutRedirectUri[1..] : postLogoutRedirectUri;

            var clientType = string.IsNullOrEmpty(request.CreateDto.ClientType)
                ? OpenIddictConstants.ClientTypes.Public
                : OpenIddictConstants.ClientTypes.Confidential;

            var appDescriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = clientId,
                ClientType = clientType,
                DisplayName = clientDisplayName,
                ApplicationType = OpenIddictConstants.ApplicationTypes.Web,
                RedirectUris = {
                    new Uri($"{clientUri}/{signinRedirectUri}"),
                    new Uri($"{clientUri}/{refreshRedirectUri}"),
                },
                PostLogoutRedirectUris = {
                    new Uri($"{clientUri}/{postLogoutRedirectUri}")
                },
                Permissions =
                {
                    // Endpoints
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Revocation,
                    OpenIddictConstants.Permissions.Endpoints.EndSession,
                    // GrantTypes
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    // Scopes
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Roles,
                    OpenIddictConstants.Permissions.Prefixes.Scope + OpenIddictConstants.Scopes.OfflineAccess,
                    // Scopes (Apis)
                    OpenIddictConstants.Permissions.Prefixes.Scope + clientApiScope
                }
            };

            var client = await openIddictApplicationManager.FindByClientIdAsync(appDescriptor.ClientId, cancellationToken);

            if (client == null)
            {
                appDescriptor.ClientSecret = clientType == OpenIddictConstants.ClientTypes.Confidential
                    ? request.CreateDto.ClientSecret
                    : null;

                await openIddictApplicationManager.CreateAsync(appDescriptor, cancellationToken);
            }
            else
            {
                if (createDto.ClientSecretUpdate)
                    appDescriptor.ClientSecret = request.CreateDto.ClientSecret;
                else
                    appDescriptor.ClientSecret = (client as OpenIddictEntityFrameworkCoreApplication)?.ClientSecret;

                appDescriptor.ClientSecret = clientType == OpenIddictConstants.ClientTypes.Confidential
                    ? request.CreateDto.ClientSecret
                    : null;

                await openIddictApplicationManager.UpdateAsync(client, appDescriptor, cancellationToken);
            }

            if (clientId != clientIdOld)
            {
                var oldClient = await openIddictApplicationManager.FindByClientIdAsync(clientIdOld, cancellationToken);
                if (oldClient != null)
                    await openIddictApplicationManager.DeleteAsync(oldClient!, cancellationToken);
            }

            return clientId;
        }
    }
}
