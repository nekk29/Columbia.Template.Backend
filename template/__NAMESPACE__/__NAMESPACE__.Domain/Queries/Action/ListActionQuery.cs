using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Action;

namespace __NAMESPACE__.Domain.Queries.Action
{
    public class ListActionQuery(Guid? moduleId = null) : QueryBase<IEnumerable<ListActionDto>>
    {
        public Guid? ModuleId { get; set; } = moduleId;
    }
}
