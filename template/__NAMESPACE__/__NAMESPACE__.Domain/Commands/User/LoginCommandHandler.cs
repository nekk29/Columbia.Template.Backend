using AutoMapper;
using FluentValidation;
using __NAMESPACE__.Common;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Domain.Extensions;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator,
        LoginCommandValidator validator,
        IConfiguration configuration,
        UserManager<Entity.ApplicationUser> userManager,
        SignInManager<Entity.ApplicationUser> signInManager,
        IRepository<Entity.Application> applicationRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : CommandHandlerBase<LoginCommand, LoginResultDto>(unitOfWork, mapper, mediator, validator)
    {
        public override async Task<ResponseDto<LoginResultDto>> HandleCommand(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<LoginResultDto>();

            var username = request.LoginDto.UserName;
            var returnUrl = request.LoginDto.ReturnUrlOrDefault();
            var returnUrlEncoded = UriUtils.EncodeUri(returnUrl);
            var lockoutOnFailure = configuration.GetValue<bool>("SignInOptions:LockoutEnabled");

            var applicationUser = await userManager.FindByNameAsync(username!);
            applicationUser ??= await userManager.FindByEmailAsync(username!);

            if (applicationUser == null)
                return ResponseWithError(response, Resources.User.UserDoesNotExist);

            if (!applicationUser.IsActive)
                return ResponseWithError(response, Resources.User.UserDisabled);

            var applicationValidation = await ValidateApplicationAccessAsync(applicationUser, request.LoginDto);
            if (!string.IsNullOrEmpty(applicationValidation))
                return ResponseWithError(response, applicationValidation);

            var result = await signInManager.PasswordSignInAsync(applicationUser?.UserName!, request.LoginDto.Password!, request.LoginDto.RememberMe, lockoutOnFailure: lockoutOnFailure);

            if (!result.Succeeded)
            {
                var isEmailConfirmed = await userManager.IsEmailConfirmedAsync(applicationUser!);

                if (userManager.Options.SignIn.RequireConfirmedAccount && !isEmailConfirmed)
                    return ResponseWithError(response, Resources.User.LoginEmailNotConfirmed);

                if (result.RequiresTwoFactor)
                {
                    var frontUrl = configuration.GetValue<string>("SecurityOptions:FrontUrl");

                    response = ResponseWithError(response, Resources.User.Login2FARequired);
                    response.Data!.ReturnUrl = $"{frontUrl}/user/login-with-2fa?returnUrl={returnUrlEncoded}";

                    return response;
                }

                if (result.IsLockedOut)
                {
                    var lockoutAttempts = configuration.GetValue<int>("SignInOptions:LockoutMaxFailedAccessAttempts");
                    var lockoutTimeInMinutes = configuration.GetValue<int>("SignInOptions:LockoutDefaultTimeSpanInMinutes");
                    var resultMessage = string.Format(Resources.User.LoginLockout, lockoutAttempts, lockoutTimeInMinutes);

                    return ResponseWithError(response, resultMessage);
                }
                
                return ResponseWithError(response, Resources.User.LoginFailed);
            }

            var tokenResult = await _mediator!.Send(
                new GenerateTokenCommand(request.LoginDto.ApplicationCode!, applicationUser!, request.LoginDto.Scopes!),
                cancellationToken
            );

            var loginResultDto = new LoginResultDto
            {
                ClaimsPrincipal = tokenResult.Data!,
                ReturnUrl = returnUrl
            };

            response.UpdateData(loginResultDto);
            response.AddOkResult(Resources.User.LoginSucceeded);

            return response;
        }

        private static ResponseDto<LoginResultDto> ResponseWithError(ResponseDto<LoginResultDto> response, string errorDescription)
        {
            response.Data = new LoginResultDto
            {
                AuthProperties = new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = errorDescription
                }
            };

            response.AddErrorResult(errorDescription);

            return response;
        }

        protected async Task<string> ValidateApplicationAccessAsync(Entity.ApplicationUser applicationUser, LoginDto loginDto)
        {
            var application = await applicationRepository.GetByAsNoTrackingAsync(x => x.IsActive && x.Code == loginDto.ApplicationCode);
            if (application == null)
                return Resources.User.NoAccessToApplication;

            var roles = await userManager.GetRolesAsync(applicationUser!);

            roles = await applicationRoleRepository
                .FindAllAsNoTracking()
                .Where(x => x.ApplicationId == application.Id && roles.Contains(x.Name!))
                .Select(x => x.Name!)
                .ToListAsync() ?? [];

            if (!roles.Any())
                return Resources.User.NoAccessToApplication;

            return null!;
        }
    }
}
