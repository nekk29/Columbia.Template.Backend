using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Role;

namespace __NAMESPACE__.Domain.Queries.Role
{
    public class SearchRoleQuery(SearchParamsDto<SearchRoleFilterDto> searchParams) : SearchQueryBase<SearchRoleFilterDto, SearchRoleDto>(searchParams)
    {

    }
}
