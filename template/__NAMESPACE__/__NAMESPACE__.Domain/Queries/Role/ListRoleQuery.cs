using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Role;

namespace __NAMESPACE__.Domain.Queries.Role
{
    public class ListRoleQuery(Guid? applicationId = null) : QueryBase<IEnumerable<ListRoleDto>>
    {
        public Guid? ApplicationId { get; set; } = applicationId;
    }
}
