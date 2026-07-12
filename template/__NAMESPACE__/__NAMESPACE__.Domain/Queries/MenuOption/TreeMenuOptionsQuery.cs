using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.MenuOption;

namespace __NAMESPACE__.Domain.MenuOption
{
    public class TreeMenuOptionsQuery(string applicationCode, bool includeAll = false) : QueryBase<IEnumerable<TreeMenuOptionDto>>
    {
        public string ApplicationCode { get; set; } = applicationCode;
        public bool IncludeAll { get; set; } = includeAll;
    }
}
