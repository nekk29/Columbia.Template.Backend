using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.User;
using Microsoft.AspNetCore.Identity;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class CreateUserCommandValidator : CommandValidatorBase<CreateUserCommand>
    {
        private readonly UserManager<Entity.ApplicationUser> _userManager;

        public CreateUserCommandValidator(UserManager<Entity.ApplicationUser> userManager)
        {
            _userManager = userManager;

            RequiredInformation(x => x.CreateDto)
                .DependentRules(() =>
                {
                    RequiredString(x => x.CreateDto.UserName, Resources.User.UserName, 6, 256);
                    RequiredString(x => x.CreateDto.Email, Resources.User.Email, 6, 256);
                    RequiredString(x => x.CreateDto.PhoneNumber, Resources.User.PhoneNumber, 2, 64);
                    RequiredString(x => x.CreateDto.FirstName, Resources.User.FirstName, 2, 100);
                    RequiredString(x => x.CreateDto.LastName, Resources.User.LastName, 2, 100);
                    RequiredString(x => x.CreateDto.Password, Resources.User.Password, 2, 256);
                    RequiredString(x => x.CreateDto.ConfirmPassword, Resources.User.ConfirmPassword, 2, 256);
                    RuleFor(x => x.CreateDto)
                        .Must(ValidatePasswordAsync)
                        .WithCustomValidationMessage();
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.CreateDto)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(CreateUserCommand command, CreateUserDto createDto, ValidationContext<CreateUserCommand> context, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByNameAsync(createDto.UserName!);

            applicationUser ??= await _userManager.FindByEmailAsync(createDto.Email!);

            if (applicationUser != null)
                return CustomValidationMessage(context, Resources.Common.DuplicateRecord);

            return true;
        }

        protected bool ValidatePasswordAsync(CreateUserCommand command, CreateUserDto dto, ValidationContext<CreateUserCommand> context)
        {
            if (string.IsNullOrEmpty(dto.Password)) return true;
            if (string.IsNullOrEmpty(dto.ConfirmPassword)) return true;

            if (string.Compare(dto.Password, dto.ConfirmPassword) != 0)
                return CustomValidationMessage(context, Resources.User.ResetPasswordNoMatch);

            return true;
        }
    }
}
