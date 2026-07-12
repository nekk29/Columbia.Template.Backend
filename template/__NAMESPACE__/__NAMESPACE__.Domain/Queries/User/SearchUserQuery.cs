using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.Domain.Queries.User
{
    public class SearchUserQuery(SearchParamsDto<SearchUserFilterDto> searchParams) : SearchQueryBase<SearchUserFilterDto, SearchUserDto>(searchParams)
    {

    }
}
