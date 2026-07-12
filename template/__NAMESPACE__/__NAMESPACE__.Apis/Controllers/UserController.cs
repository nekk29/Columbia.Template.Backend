using __NAMESPACE__.Apis.Controllers.Base;
using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Repository.Abstractions.Security;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace __NAMESPACE__.Apis.Controllers
{
    [ApiController]
    [Security.Authorize]
    [Route("api/user")]
    public class UserController(
        IUserIdentity userIdentity,
        IServiceProvider serviceProvider,
        IUserApplication userApplication
    ) : ApiControllerBase(serviceProvider)
    {
        [HttpPost]
        public async Task<ResponseDto<GetUserDto>> Create(CreateUserDto createDto)
            => await userApplication.Create(createDto);

        [HttpPut]
        public async Task<ResponseDto<GetUserDto>> Update(UpdateUserDto updateDto)
            => await userApplication.Update(updateDto);

        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete(Guid id)
            => await userApplication.Delete(id);

        [HttpGet("{id}")]
        public async Task<ResponseDto<GetUserDto>> Get(Guid id)
            => await userApplication.Get(id);

        [HttpGet("list")]
        public async Task<ResponseDto<IEnumerable<ListUserDto>>> List()
            => await userApplication.List();

        [HttpPost("search")]
        public async Task<ResponseDto<SearchResultDto<SearchUserDto>>> Search(SearchParamsDto<SearchUserFilterDto> searchParams)
            => await userApplication.Search(searchParams);

        [AllowAnonymous]
        [HttpPost("login")]
        [Produces("application/json")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Login()
        {
            var request = HttpContext.GetOpenIddictServerRequest()
                    ?? throw new InvalidOperationException("The OpenIddict server request cannot be retrieved.");

            if (request.IsPasswordGrantType())
            {
                var headers = HttpContext.Request.Headers;
                var applicationCode = headers["X-ApplicationCode"];
                var rememberMe = headers["X-RememberMe"] == "true";
                var returnUrl = headers["X-ReturnUrl"];

                var loginDto = new LoginDto
                {
                    ApplicationCode = applicationCode,
                    UserName = request.Username,
                    Password = request.Password,
                    RememberMe = rememberMe,
                    ReturnUrl = returnUrl!,
                    Scopes = request.GetScopes()
                };

                var result = await userApplication.Login(loginDto);

                if (!result.IsValid)
                {
                    var properties = new AuthenticationProperties(result.Data?.AuthProperties ?? []);
                    return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                return SignIn(result.Data!.ClaimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            if (request.IsRefreshTokenGrantType())
            {
                var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                if (result.Succeeded)
                    return SignIn(result.Principal!, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var properties = new Dictionary<string, string?> {
                    { OpenIddictServerAspNetCoreConstants.Properties.Error, Errors.InvalidGrant },
                    { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, result.Failure?.Message },
                };

                return Forbid(new AuthenticationProperties(properties), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        [HttpGet("info")]
        [Produces("application/json")]
        public async Task<IActionResult> UserInfo()
        {
            var userId = userIdentity.GetSubject();
            var user = await userApplication.Get(userId);

            if (user == null) return Challenge();
            if (user.Data == null) return Challenge();
            if (!user.IsValid) return Challenge();

            var claims = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                [Claims.Subject] = user.Data.Id,
                [Claims.Email] = user.Data.Email!,
                [Claims.Username] = user.Data.UserName!,
                [Claims.Name] = user.Data.FirstName!,
                [Claims.FamilyName] = user.Data.LastName!,
                [Claims.Nickname] = $"{user.Data.FirstName} {user.Data.LastName}",
                [Claims.PhoneNumber] = user.Data.PhoneNumber!,
            };

            return Ok(claims);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ResponseDto> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
            => await userApplication.ForgotPassword(forgotPasswordDto);

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ResponseDto> ResetPassword(ResetPasswordDto resetPasswordDto)
            => await userApplication.ResetPassword(resetPasswordDto);
    }
}
