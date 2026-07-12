using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.Domain.Queries.User
{
    public class GetUserQuery(Guid id) : QueryBase<GetUserDto>
    {
        public Guid Id { get; set; } = id;
    }
}
