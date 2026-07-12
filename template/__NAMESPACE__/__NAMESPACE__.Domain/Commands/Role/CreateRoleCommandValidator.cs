using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Role;
using __NAMESPACE__.Repository.Abstractions.Base;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class CreateRoleCommandValidator : CommandValidatorBase<CreateRoleCommand>
    {
        private readonly IRepository<Entity.ApplicationRole> _applicationRoleRepository;

        public CreateRoleCommandValidator(IRepository<Entity.ApplicationRole> applicationRoleRepository)
        {
            _applicationRoleRepository = applicationRoleRepository;

            RequiredInformation(x => x.CreateDto)
                .DependentRules(() =>
                {
                    RequiredString(x => x.CreateDto.Name, Resources.Role.Name, 2, 100);
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.CreateDto)
                        .MustAsync(ValidateExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateExistenceAsync(CreateRoleCommand command, CreateRoleDto createDto, ValidationContext<CreateRoleCommand> context, CancellationToken cancellationToken)
        {
            var role = await _applicationRoleRepository.GetByAsNoTrackingAsync(x => x.Name == createDto.Name && x.IsActive);
            if (role != null) return CustomValidationMessage(context, Resources.Common.DuplicateRecord);
            return true;
        }
    }
}
