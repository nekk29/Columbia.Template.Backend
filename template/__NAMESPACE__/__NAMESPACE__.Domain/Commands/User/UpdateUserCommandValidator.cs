using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class UpdateUserCommandValidator : CommandValidatorBase<UpdateUserCommand>
    {
        private readonly IRepository<Entity.ApplicationUser> _applicationUserRepository;

        public UpdateUserCommandValidator(IRepository<Entity.ApplicationUser> applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;

            RequiredInformation(x => x.UpdateDto)
                .DependentRules(() =>
                {
                    RequiredField(x => x.UpdateDto.Id, Resources.User.Id);
                    RequiredString(x => x.UpdateDto.UserName, Resources.User.UserName, 6, 256);
                    RequiredString(x => x.UpdateDto.Email, Resources.User.Email, 6, 256);
                    RequiredString(x => x.UpdateDto.PhoneNumber, Resources.User.PhoneNumber, 2, 64);
                    RequiredString(x => x.UpdateDto.FirstName, Resources.User.FirstName, 2, 100);
                    RequiredString(x => x.UpdateDto.LastName, Resources.User.LastName, 2, 100);
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.UpdateDto.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(UpdateUserCommand command, Guid? id, ValidationContext<UpdateUserCommand> context, CancellationToken cancellationToken)
        {
            if (!id.HasValue) return true;
            if (string.IsNullOrEmpty(command.UpdateDto.Email)) return true;
            if (string.IsNullOrEmpty(command.UpdateDto.UserName)) return true;

            var exists = await _applicationUserRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists)
                return CustomValidationMessage(context, Resources.Common.UpdateRecordNotFound);

            exists = await _applicationUserRepository.FindAll().Where(x => x.Email == command.UpdateDto.Email && x.Id != id).AnyAsync(cancellationToken);
            if (exists)
                return CustomValidationMessage(context, Resources.User.DuplicateRecordByEmail);

            exists = await _applicationUserRepository.FindAll().Where(x => x.UserName == command.UpdateDto.Email && x.Id != id).AnyAsync(cancellationToken);
            if (exists)
                return CustomValidationMessage(context, Resources.User.DuplicateRecordByUserName);

            return true;
        }
    }
}
