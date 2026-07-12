using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Permission;

namespace __NAMESPACE__.Domain.Queries.Permission
{
    public class ListRolePermissionsQuery(Guid roleId) : QueryBase<IEnumerable<ListRolePermissionDto>>
    {
        public Guid RoleId { get; set; } = roleId;
    }
}
