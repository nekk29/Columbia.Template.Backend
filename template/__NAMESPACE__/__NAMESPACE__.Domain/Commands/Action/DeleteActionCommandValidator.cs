using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.Action
{
    public class DeleteActionCommandValidator : CommandValidatorBase<DeleteActionCommand>
    {
        private readonly IRepository<Entity.Action> _actionRepository;

        public DeleteActionCommandValidator(IRepository<Entity.Action> actionRepository)
        {
            _actionRepository = actionRepository;

            RequiredField(x => x.Id, Resources.Common.IdentifierRequired)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(DeleteActionCommand command, Guid id, ValidationContext<DeleteActionCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _actionRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Common.DeleteRecordNotFound);
            return true;
        }
    }
}
