using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class DeleteUserCommandValidator : CommandValidatorBase<DeleteUserCommand>
    {
        private readonly IRepository<Entity.ApplicationUser> _applicationUserRepository;

        public DeleteUserCommandValidator(IRepository<Entity.ApplicationUser> applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;

            RequiredField(x => x.Id, Resources.Common.IdentifierRequired)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(DeleteUserCommand command, Guid id, ValidationContext<DeleteUserCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _applicationUserRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Common.DeleteRecordNotFound);
            return true;
        }
    }
}
