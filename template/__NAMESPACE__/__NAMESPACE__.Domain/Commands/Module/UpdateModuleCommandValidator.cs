using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.Module
{
    public class UpdateModuleCommandValidator : CommandValidatorBase<UpdateModuleCommand>
    {
        private readonly IRepository<Entity.Module> _moduleRepository;

        public UpdateModuleCommandValidator(IRepository<Entity.Module> moduleRepository)
        {
            _moduleRepository = moduleRepository;

            RequiredInformation(x => x.UpdateDto)
                .DependentRules(() =>
                {
                    //RequiredField(x => x.UpdateDto.Id, Resources.Common.IdentifierRequired);
                    //RequiredField(x => x.UpdateDto.ApplicationId, Resources.Module.ApplicationId);
                    //RequiredString(x => x.UpdateDto.Code, Resources.Module.Code, {Min}, {Max});
                    //RequiredString(x => x.UpdateDto.Name, Resources.Module.Name, {Min}, {Max});
                }).DependentRules(() =>
                {
                    RuleFor(x => x.UpdateDto.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(UpdateModuleCommand command, Guid id, ValidationContext<UpdateModuleCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _moduleRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Common.UpdateRecordNotFound);
            return true;
        }
    }
}
