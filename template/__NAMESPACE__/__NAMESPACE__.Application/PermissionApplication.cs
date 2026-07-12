using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Application.Base;
using __NAMESPACE__.Domain.Commands.Permission;
using __NAMESPACE__.Domain.Queries.Permission;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Permission;
using MediatR;

namespace __NAMESPACE__.Application
{
    public class PermissionApplication(IMediator mediator) : ApplicationBase(mediator), IPermissionApplication
    {
        public async Task<ResponseDto> AssignPermissions(Guid roleId, IEnumerable<Guid> actionIds)
            => await _mediator.Send(new AssignPermissionsCommand(roleId, actionIds));

        public async Task<ResponseDto<IEnumerable<ListRolePermissionDto>>> ListRole(Guid roleId)
            => await _mediator.Send(new ListRolePermissionsQuery(roleId));

        public async Task<ResponseDto<IEnumerable<ListPermissionDto>>> ListUser(string applicationCode)
            => await _mediator.Send(new ListUserPermissionsQuery(applicationCode));
    }
}
