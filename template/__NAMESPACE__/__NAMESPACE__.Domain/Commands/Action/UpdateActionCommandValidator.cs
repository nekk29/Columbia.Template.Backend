using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.Action
{
    public class UpdateActionCommandValidator : CommandValidatorBase<UpdateActionCommand>
    {
        private readonly IRepository<Entity.Action> _actionRepository;

        public UpdateActionCommandValidator(IRepository<Entity.Action> actionRepository)
        {
            _actionRepository = actionRepository;

            RequiredInformation(x => x.UpdateDto)
                .DependentRules(() =>
                {
                    //RequiredField(x => x.UpdateDto.Id, Resources.Common.IdentifierRequired);
                    //RequiredField(x => x.UpdateDto.ModuleId, Resources.Action.ModuleId);
                    //RequiredString(x => x.UpdateDto.Code, Resources.Action.Code, {Min}, {Max});
                    //RequiredString(x => x.UpdateDto.Name, Resources.Action.Name, {Min}, {Max});
                }).DependentRules(() =>
                {
                    RuleFor(x => x.UpdateDto.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(UpdateActionCommand command, Guid id, ValidationContext<UpdateActionCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _actionRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Common.UpdateRecordNotFound);
            return true;
        }
    }
}
