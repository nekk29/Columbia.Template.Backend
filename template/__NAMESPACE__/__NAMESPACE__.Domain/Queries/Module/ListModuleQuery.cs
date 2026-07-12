using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Module;

namespace __NAMESPACE__.Domain.Queries.Module
{
    public class ListModuleQuery(Guid? applicationId = null, bool includeActions = true) : QueryBase<IEnumerable<ListModuleDto>>
    {
        public Guid? ApplicationId { get; set; } = applicationId;
        public bool IncludeActions { get; set; } = includeActions;
    }
}
