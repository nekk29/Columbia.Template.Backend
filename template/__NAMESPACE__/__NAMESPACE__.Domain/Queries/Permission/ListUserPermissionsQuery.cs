using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Permission;

namespace __NAMESPACE__.Domain.Queries.Permission
{
    public class ListUserPermissionsQuery(string applicationCode) : QueryBase<IEnumerable<ListPermissionDto>>
    {
        public string ApplicationCode { get; set; } = applicationCode;
    }
}
