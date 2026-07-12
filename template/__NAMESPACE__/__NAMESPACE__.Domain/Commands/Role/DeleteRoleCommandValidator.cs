using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class DeleteRoleCommandValidator : CommandValidatorBase<DeleteRoleCommand>
    {
        private readonly IRepository<Entity.ApplicationRole> _applicationRoleRepository;

        public DeleteRoleCommandValidator(IRepository<Entity.ApplicationRole> applicationRoleRepository)
        {
            _applicationRoleRepository = applicationRoleRepository;

            RequiredField(x => x.Id, Resources.Common.IdentifierRequired)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(DeleteRoleCommand command, Guid id, ValidationContext<DeleteRoleCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _applicationRoleRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Common.DeleteRecordNotFound);
            return true;
        }
    }
}
