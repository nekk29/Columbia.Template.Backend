using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Permission;

namespace __NAMESPACE__.Application.Abstractions
{
    public interface IPermissionApplication
    {
        Task<ResponseDto> AssignPermissions(Guid roleId, IEnumerable<Guid> actionIds);
        Task<ResponseDto<IEnumerable<ListRolePermissionDto>>> ListRole(Guid roleId);
        Task<ResponseDto<IEnumerable<ListPermissionDto>>> ListUser(string applicationCode);
    }
}
