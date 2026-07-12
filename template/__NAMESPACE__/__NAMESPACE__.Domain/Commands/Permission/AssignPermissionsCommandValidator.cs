using FluentValidation;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Commands.Permission
{
    public class AssignPermissionsCommandValidator : CommandValidatorBase<AssignPermissionsCommand>
    {
        private readonly IRepository<Entity.Action> _actionRepository;
        private readonly IRepository<Entity.ApplicationRole> _applicationRoleRepository;

        public AssignPermissionsCommandValidator(
            IRepository<Entity.Action> actionRepository,
            IRepository<Entity.ApplicationRole> applicationRoleRepository
        )
        {
            _actionRepository = actionRepository;
            _applicationRoleRepository = applicationRoleRepository;

            RequiredField(x => x.RoleId, Resources.Role.RoleId)
                .DependentRules(() =>
                {
                    RuleFor(x => x.RoleId)
                        .MustAsync(ValidateRoleExistenceAsync)
                        .WithCustomValidationMessage();
                });
        }

        protected async Task<bool> ValidateRoleExistenceAsync(AssignPermissionsCommand command, Guid id, ValidationContext<AssignPermissionsCommand> context, CancellationToken cancellationToken)
        {
            var exists = await _applicationRoleRepository.FindAll().Where(x => x.Id == id).AnyAsync(cancellationToken);
            if (!exists) return CustomValidationMessage(context, Resources.Role.RoleDoesNotExist);
            return true;
        }

        protected async Task<bool> ValidateActionsExistenceAsync(AssignPermissionsCommand command, IEnumerable<Guid> actionIds, ValidationContext<AssignPermissionsCommand> context, CancellationToken cancellationToken)
        {
            var uniqueActionIds = actionIds.Distinct();

            var actionsCount = await _actionRepository
                .FindAll()
                .Where(x => uniqueActionIds.Contains(x.Id) && x.IsActive)
                .CountAsync(cancellationToken);

            if (actionsCount < uniqueActionIds.Count())
                return CustomValidationMessage(context, Resources.Action.ActionDoesNotExist);

            return true;
        }
    }
}
