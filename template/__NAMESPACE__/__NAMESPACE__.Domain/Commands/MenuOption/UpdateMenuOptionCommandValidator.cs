using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.MenuOption
{
    public class UpdateMenuOptionCommandValidator : CommandValidatorBase<UpdateMenuOptionCommand>
    {
        private readonly IRepository<Entity.MenuOption> _menuOptionRepository;

        public UpdateMenuOptionCommandValidator(IRepository<Entity.MenuOption> menuOptionRepository)
        {
            _menuOptionRepository = menuOptionRepository;

            RequiredInformation(x => x.UpdateDto)
                .DependentRules(() =>
                {
                    //RequiredField(x => x.UpdateDto.Id, Resources.Common.IdentifierRequired);
                    //RequiredField(x => x.UpdateDto.ApplicationId, Resources.MenuOption.ApplicationId);
                    //RequiredField(x => x.UpdateDto.ActionId, Resources.MenuOption.ActionId);
                    //RequiredString(x => x.UpdateDto.Code, Resources.MenuOption.Code, {Min}, {Max});
                    //RequiredString(x => x.UpdateDto.Name, Resources.MenuOption.Name, {Min}, {Max});
                    //RequiredField(x => x.UpdateDto.SortOrder, Resources.MenuOption.SortOrder);
                }).DependentRules(() =>
                {
                    RuleFor(x => x.UpdateDto.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(UpdateMenuOptionCommand command, Guid id, ValidationContext<UpdateMenuOptionCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _menuOptionRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Common.UpdateRecordNotFound);
            return true;
        }
    }
}
