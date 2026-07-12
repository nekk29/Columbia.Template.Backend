using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.Domain.Queries.Application
{
    public class SearchApplicationQuery(SearchParamsDto<SearchApplicationFilterDto> searchParams) : SearchQueryBase<SearchApplicationFilterDto, SearchApplicationDto>(searchParams)
    {

    }
}
