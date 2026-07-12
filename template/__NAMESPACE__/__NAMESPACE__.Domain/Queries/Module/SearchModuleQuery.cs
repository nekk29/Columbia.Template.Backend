using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Module;

namespace __NAMESPACE__.Domain.Queries.Module
{
    public class SearchModuleQuery(SearchParamsDto<SearchModuleFilterDto> searchParams) : SearchQueryBase<SearchModuleFilterDto, SearchModuleDto>(searchParams)
    {

    }
}
