using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using Microsoft.AspNetCore.Identity;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class ForgotPasswordCommandValidator : CommandValidatorBase<ForgotPasswordCommand>
    {
        private readonly UserManager<Entity.ApplicationUser> _userManager;

        public ForgotPasswordCommandValidator(UserManager<Entity.ApplicationUser> userManager)
        {
            _userManager = userManager;

            RequiredInformation(x => x.ForgotPasswordDto)
                .DependentRules(() =>
                {
                    RequiredString(x => x.ForgotPasswordDto.Email, Resources.User.Email, default, 256)
                        .DependentRules(() =>
                        {
                            ValidMail(x => x.ForgotPasswordDto.Email, Resources.User.Email)
                                .DependentRules(() =>
                                {
                                    RuleFor(x => x.ForgotPasswordDto.Email)
                                        .MustAsync(ValidateExistenceAsync)
                                        .WithCustomValidationMessage();
                                });
                        });
                });
        }

        protected async Task<bool> ValidateExistenceAsync(ForgotPasswordCommand command, string email, ValidationContext<ForgotPasswordCommand> context, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser == null)
                return CustomValidationMessage(context, Resources.User.UserDoesNotExist);

            if (!applicationUser.IsActive)
                return CustomValidationMessage(context, Resources.User.UserDisabled);

            return true;
        }
    }
}
