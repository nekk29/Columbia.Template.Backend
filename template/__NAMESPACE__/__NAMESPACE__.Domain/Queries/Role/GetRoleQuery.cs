using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Role;

namespace __NAMESPACE__.Domain.Queries.Role
{
    public class GetRoleQuery(Guid id) : QueryBase<GetRoleDto>
    {
        public Guid Id { get; set; } = id;
    }
}
