using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.MenuOption;

namespace __NAMESPACE__.Domain.Queries.MenuOption
{
    public class SearchMenuOptionQuery(SearchParamsDto<SearchMenuOptionFilterDto> searchParams) : SearchQueryBase<SearchMenuOptionFilterDto, SearchMenuOptionDto>(searchParams)
    {

    }
}
