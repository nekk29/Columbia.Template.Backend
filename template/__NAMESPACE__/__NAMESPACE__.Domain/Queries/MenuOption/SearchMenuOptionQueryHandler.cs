using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Extensions;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.MenuOption;
using __NAMESPACE__.Repository.Abstractions.Base;

namespace __NAMESPACE__.Domain.Queries.MenuOption
{
    public class SearchMenuOptionQueryHandler(
        IMapper mapper,
        IRepository<Entity.MenuOption> menuOptionRepository
    ) : SearchQueryHandlerBase<SearchMenuOptionQuery, SearchMenuOptionFilterDto, SearchMenuOptionDto>(mapper)
    {
        protected override async Task<ResponseDto<SearchResultDto<SearchMenuOptionDto>>> HandleQuery(SearchMenuOptionQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<SearchResultDto<SearchMenuOptionDto>>();

            Expression<Func<Entity.MenuOption, bool>> filter = x => true;

            var filters = request.SearchParams?.Filter;

            var menuOptions = await menuOptionRepository.SearchByAsNoTrackingAsync(
                request.SearchParams?.Page?.Page ?? 1,
                request.SearchParams?.Page?.PageSize ?? 10,
                request.SearchParams?.Sort?.GetSortExpressions<Entity.MenuOption>(),
                filter,
                x => x.Application,
                x => x.ParentMenuOption!,
                x => x.Action.Module
            );

            var menuOptionDtos = _mapper?.Map<IEnumerable<SearchMenuOptionDto>>(menuOptions.Items);

            var searchResult = new SearchResultDto<SearchMenuOptionDto>(
                menuOptionDtos ?? [],
                menuOptions.Total,
                request.SearchParams
            );

            response.UpdateData(searchResult);

            return await Task.FromResult(response);
        }
    }
}
