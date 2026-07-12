using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.User;
using Microsoft.AspNetCore.Identity;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class ResetPasswordCommandValidator : CommandValidatorBase<ResetPasswordCommand>
    {
        private readonly UserManager<Entity.ApplicationUser> _userManager;

        public ResetPasswordCommandValidator(UserManager<Entity.ApplicationUser> userManager)
        {
            _userManager = userManager;

            RequiredInformation(x => x.ResetPasswordDto)
                .WithCustomValidationMessage()
                .DependentRules(() =>
                {
                    RequiredString(x => x.ResetPasswordDto.Email, Resources.User.Email, default, 256);
                    ValidMail(x => x.ResetPasswordDto.Email, Resources.User.Email);

                    RequiredField(x => x.ResetPasswordDto.Code, Resources.User.ResetPasswordCode);
                    RequiredString(x => x.ResetPasswordDto.Password, Resources.User.Password, 2, 100);
                    RequiredString(x => x.ResetPasswordDto.ConfirmPassword, Resources.User.ConfirmPassword, 2, 100);

                    RuleFor(x => x.ResetPasswordDto)
                        .Must(ValidatePasswordAsync)
                        .WithCustomValidationMessage();
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.ResetPasswordDto.Email)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(ResetPasswordCommand command, string email, ValidationContext<ResetPasswordCommand> context, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser == null)
                return CustomValidationMessage(context, Resources.User.UserDoesNotExist);

            if (!applicationUser.IsActive)
                return CustomValidationMessage(context, Resources.User.UserDisabled);

            return true;
        }

        protected bool ValidatePasswordAsync(ResetPasswordCommand command, ResetPasswordDto dto, ValidationContext<ResetPasswordCommand> context)
        {
            if (string.IsNullOrEmpty(dto.Password)) return true;
            if (string.IsNullOrEmpty(dto.ConfirmPassword)) return true;

            if (string.Compare(dto.Password, dto.ConfirmPassword) != 0)
                return CustomValidationMessage(context, Resources.User.ResetPasswordNoMatch);

            return true;
        }
    }
}
