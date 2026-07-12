using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.Module
{
    public class DeleteModuleCommandValidator : CommandValidatorBase<DeleteModuleCommand>
    {
        private readonly IRepository<Entity.Module> _moduleRepository;

        public DeleteModuleCommandValidator(IRepository<Entity.Module> moduleRepository)
        {
            _moduleRepository = moduleRepository;

            RequiredField(x => x.Id, Resources.Common.IdentifierRequired)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(DeleteModuleCommand command, Guid id, ValidationContext<DeleteModuleCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _moduleRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Common.DeleteRecordNotFound);
            return true;
        }
    }
}
