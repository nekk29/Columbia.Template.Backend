using System.Net;
using System.Text;
using AutoMapper;
using __NAMESPACE__.Common;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Domain.Commands.Email;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Email;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Entity;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class ForgotPasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator,
        ForgotPasswordCommandValidator validator,
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        ILogger<ForgotPasswordCommandHandler> logger
    ) : CommandHandlerBase<ForgotPasswordCommand>(unitOfWork, mapper, mediator, validator)
    {
        public override async Task<ResponseDto> HandleCommand(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<LoginResultDto>();

            var user = await userManager.FindByEmailAsync(request.ForgotPasswordDto.Email);
            var code = await userManager.GeneratePasswordResetTokenAsync(user!);

            try
            {
                await SendResetPasswordEmail(user!, code);
                response.AddOkResult(Resources.User.ForgotPasswordSuccess);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Message: {Message}", ex.Message);
                response.AddErrorResult(Resources.User.ForgotPasswordError);
            }

            return response;
        }

        public async Task SendResetPasswordEmail(ApplicationUser user, string code)
        {
            var email = WebUtility.UrlDecode(user.Email);
            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var frontUrl = configuration.GetValue<string>("SecurityOptions:FrontUrl");
            var frontUrlLogo = configuration.GetValue<string>("SecurityOptions:FrontUrlLogo");
            var callbackUrl = $"{frontUrl}/user/reset-password?email={email}&code={encoded}";

            var emailDto = new SendEmailDto
            {
                EmailCode = Constants.Email.User.ResetPassword,
                ToEmails = new List<string> { user.Email ?? string.Empty },
                BodyParams = new Dictionary<string, string>
                {
                    { "{LOGO}", frontUrlLogo! },
                    { "{USER}", user.FirstName! },
                    { "{LINK}", callbackUrl }
                }
            };

            await _mediator!.Send(new SendEmailCommand(emailDto));
        }
    }
}
