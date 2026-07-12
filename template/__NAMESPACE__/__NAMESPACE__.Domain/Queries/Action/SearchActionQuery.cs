using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.Domain.Queries.Action
{
    public class SearchActionQuery(SearchParamsDto<SearchActionFilterDto> searchParams) : SearchQueryBase<SearchActionFilterDto, SearchActionDto>(searchParams)
    {

    }
}
