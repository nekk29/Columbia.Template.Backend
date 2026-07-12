using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class UpdateRoleCommandValidator : CommandValidatorBase<UpdateRoleCommand>
    {
        private readonly IRepository<Entity.ApplicationRole> _applicationRoleRepository;

        public UpdateRoleCommandValidator(IRepository<Entity.ApplicationRole> applicationRoleRepository)
        {
            _applicationRoleRepository = applicationRoleRepository;

            RequiredInformation(x => x.UpdateDto)
                .DependentRules(() =>
                {
                    RequiredField(x => x.UpdateDto.Id, Resources.Role.Id);
                    RequiredString(x => x.UpdateDto.Name, Resources.Role.Name, 2, 100);
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.UpdateDto.Id)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(UpdateRoleCommand command, Guid id, ValidationContext<UpdateRoleCommand> context, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.UpdateDto.Name)) return true;
            if (string.IsNullOrEmpty(command.UpdateDto.NormalizedName)) return true;

            var exists = await _applicationRoleRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists)
                return CustomValidationMessage(context, Resources.Common.UpdateRecordNotFound);

            exists = await _applicationRoleRepository.FindAll().Where(x => x.Name == command.UpdateDto.Name && x.Id != id).AnyAsync(cancellationToken);
            if (exists)
                return CustomValidationMessage(context, Resources.Role.DuplicateRecordByRoleName);

            return true;
        }
    }
}
