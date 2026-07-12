using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

namespace __NAMESPACE__.Domain.Queries.Application
{
    public class GetApplicationQueryHandler(
        IMapper mapper,
        IRepository<Entity.Application> applicationRepository,
        IOpenIddictApplicationManager openIddictApplicationManager
    ) : QueryHandlerBase<GetApplicationQuery, GetApplicationDto>(mapper)
    {
        protected override async Task<ResponseDto<GetApplicationDto>> HandleQuery(GetApplicationQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetApplicationDto>();
            var application = await applicationRepository.GetByAsync(x => x.Id == request.Id);
            var applicationDto = _mapper?.Map<GetApplicationDto>(application);

            if (applicationDto != null)
            {
                if (!string.IsNullOrEmpty(applicationDto.ClientId))
                {
                    var oidcApplication = await openIddictApplicationManager
                        .FindByClientIdAsync(applicationDto.ClientId, cancellationToken)
                        as OpenIddictEntityFrameworkCoreApplication;
                        
                    if (oidcApplication != null)
                    {
                        applicationDto.IncludeClient = true;
                        applicationDto.SigninRedirectUri = GetApplicationUris(oidcApplication.RedirectUris!).FirstOrDefault(x => x.Contains("signin"))?.Trim();
                        applicationDto.SigninRedirectUri = applicationDto.SigninRedirectUri?.Replace($"{applicationDto.ApplicationUri}", string.Empty);
                        applicationDto.RefreshRedirectUri = GetApplicationUris(oidcApplication.RedirectUris!).FirstOrDefault(x => x.Contains("refresh"))?.Trim();
                        applicationDto.RefreshRedirectUri = applicationDto.RefreshRedirectUri?.Replace($"{applicationDto.ApplicationUri}", string.Empty);
                        applicationDto.PostLogoutRedirectUri = GetApplicationUris(oidcApplication.PostLogoutRedirectUris!).FirstOrDefault(x => x.Contains("signout"))?.Trim();
                        applicationDto.PostLogoutRedirectUri = applicationDto.PostLogoutRedirectUri?.Replace($"{applicationDto.ApplicationUri}", string.Empty);
                        applicationDto.ClientSecret = string.Empty;
                        applicationDto.ClientSecretUpdate = false;
                    }
                }

                response.UpdateData(applicationDto);
            }

            return await Task.FromResult(response);
        }

        private static IEnumerable<string> GetApplicationUris(string uris)
        {
            if (string.IsNullOrEmpty(uris)) return [];

            try
            {
                var urisJJsonArray = System.Text.Json.JsonSerializer.Deserialize<string[]>(uris);
                return urisJJsonArray ?? [];
            }
            catch { return []; }
        }
    }
}
