using AutoMapper;
using __NAMESPACE__.Common;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Domain.Commands.Email;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Email;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator,
        CreateUserCommandValidator validator,
        IConfiguration configuration,
        ILogger<CreateUserCommandHandler> logger,
        UserManager<Entity.ApplicationUser> userManager,
        IRepository<Entity.ApplicationUser> applicationUserRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : CommandHandlerBase<CreateUserCommand, GetUserDto>(unitOfWork, mapper, mediator, validator)
    {
        protected override bool UseTransaction => false;

        public override async Task<ResponseDto<GetUserDto>> HandleCommand(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetUserDto>();

            var applicationUser = _mapper?.Map<Entity.ApplicationUser>(request.CreateDto);

            if (applicationUser != null)
            {
                applicationUser.EmailConfirmed = true;

                applicationUserRepository.UpdateAuditTrails(applicationUser);

                var result = await userManager.CreateAsync(applicationUser, request.CreateDto.Password!);

                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e =>
                    {
                        response.AddErrorResult($"{e.Code}: {e.Description}");
                    });

                    return response;
                }

                if (response.IsValid)
                    response.AddOkResult(Resources.Common.CreateSuccessMessage);

                var roleIds = request.CreateDto.RoleIds ?? [];
                var roles = await applicationRoleRepository.FindByAsNoTrackingAsync(x => roleIds.Contains(x.Id));

                if (roles.Any())
                {
                    var addRolesResult = await userManager.AddToRolesAsync(applicationUser, roles.Select(x => x.NormalizedName)!);
                    if (!addRolesResult.Succeeded)
                        addRolesResult.Errors.ToList().ForEach(e => { response.AddErrorResult($"{e.Code}: {e.Description}"); });
                }

                try
                {
                    await SendCreationEmail(request);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Message: {Message}", ex.Message);
                    response.AddWarningResult(Resources.User.CreateUserMailError);
                }

                var getUserDto = _mapper?.Map<GetUserDto>(applicationUser);
                if (getUserDto != null) response.UpdateData(getUserDto);
            }

            return response;
        }

        public async Task SendCreationEmail(CreateUserCommand request)
        {
            var sendMail = configuration.GetValue<bool>("SignInOptions:SendMailOnSignUp");
            if (sendMail)
            {
                var application = configuration.GetValue<string>("ApiOptions:Name");
                var frontUrlLogo = configuration.GetValue<string>("SecurityOptions:FrontUrlLogo");

                var emailDto = new SendEmailDto
                {
                    EmailCode = Constants.Email.User.Registration,
                    ToEmails = new List<string> { request.CreateDto?.Email ?? string.Empty },
                    BodyParams = new Dictionary<string, string>
                    {
                        { "{APPLICATION}", application! },
                        { "{LOGO}", frontUrlLogo! },
                        { "{USER}", request.CreateDto?.UserName! },
                        { "{PASSWORD}", request.CreateDto?.Password! }
                    }
                };

                await _mediator!.Send(new SendEmailCommand(emailDto));
            }
        }
    }
}
