using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Action;

namespace __NAMESPACE__.Domain.Queries.Action
{
    public class GetActionQuery(Guid id) : QueryBase<GetActionDto>
    {
        public Guid Id { get; set; } = id;
    }
}
