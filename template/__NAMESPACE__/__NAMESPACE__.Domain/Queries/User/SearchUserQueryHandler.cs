using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Extensions;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Extensions;

namespace __NAMESPACE__.Domain.Queries.User
{
    public class SearchUserQueryHandler(
        IMapper mapper,
        IRepository<Entity.ApplicationUser> applicationUserRepository
    ) : SearchQueryHandlerBase<SearchUserQuery, SearchUserFilterDto, SearchUserDto>(mapper)
    {
        protected override async Task<ResponseDto<SearchResultDto<SearchUserDto>>> HandleQuery(SearchUserQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<SearchResultDto<SearchUserDto>>();

            Expression<Func<Entity.ApplicationUser, bool>> filter = x => true;

            var filters = request.SearchParams?.Filter;

            if (!string.IsNullOrEmpty(filters?.Query))
            {
                filter = filter.And(x =>
                    x.UserName!.Contains(filters.Query!) ||
                    x.FirstName!.Contains(filters.Query!) ||
                    x.LastName!.Contains(filters.Query!) ||
                    x.Email!.Contains(filters.Query!) ||
                    x.PhoneNumber!.Contains(filters.Query!)
                );
            }

            var users = await applicationUserRepository.SearchByAsNoTrackingAsync(
                request.SearchParams?.Page?.Page ?? 1,
                request.SearchParams?.Page?.PageSize ?? 10,
                request.SearchParams?.Sort?.GetSortExpressions<Entity.ApplicationUser>(),
                filter
            );

            var userDtos = _mapper?.Map<IEnumerable<SearchUserDto>>(users.Items);

            var searchResult = new SearchResultDto<SearchUserDto>(
                userDtos ?? [],
                users.Total,
                request.SearchParams
            );

            response.UpdateData(searchResult);

            return await Task.FromResult(response);
        }
    }
}
