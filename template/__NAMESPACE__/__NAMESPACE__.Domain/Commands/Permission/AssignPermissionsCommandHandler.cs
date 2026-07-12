using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using MediatR;

namespace __NAMESPACE__.Domain.Commands.Permission
{
    public class AssignPermissionsCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator,
        AssignPermissionsCommandValidator validator,
        IRepository<Entity.Permission> permissionRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : CommandHandlerBase<AssignPermissionsCommand>(unitOfWork, mapper, mediator, validator)
    {
        public override async Task<ResponseDto> HandleCommand(AssignPermissionsCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();

            var role = await applicationRoleRepository.GetByAsNoTrackingAsync(x => x.Id == request.RoleId);
            if (role == null)
                return response;

            var permissions = await permissionRepository.FindByAsNoTrackingAsync(
                x => x.RoleId == role.Id && x.IsActive
            );

            var uniqueActionIds = request.ActionIds.Distinct();
            var permissionsActionIds = permissions.Select(x => x.ActionId);
            var permissionsToDelete = permissions.Where(x => !uniqueActionIds.Contains(x.ActionId));

            await permissionRepository.DeleteAsync(permissionsToDelete.ToArray());

            var permissionsActionIdsToAdd = uniqueActionIds.Where(x => !permissionsActionIds.Contains(x));
            var permissionsActionToAdd = permissionsActionIdsToAdd.Select(x => new Entity.Permission
            {
                RoleId = request.RoleId,
                ActionId = x
            });

            await permissionRepository.AddAsync(permissionsActionToAdd.ToArray());

            response.AddOkResult(Resources.Permission.AssignPermissionsSuccess);

            return response;
        }
    }
}
