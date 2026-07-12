using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.MenuOption;

namespace __NAMESPACE__.Domain.Queries.MenuOption
{
    public class ListMenuOptionQuery(string applicationCode, bool includeAll = false) : QueryBase<IEnumerable<ListMenuOptionDto>>
    {
        public string ApplicationCode { get; set; } = applicationCode;
        public bool IncludeAll { get; set; } = includeAll;
    }
}
