using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Extensions;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;

namespace __NAMESPACE__.Domain.Queries.Action
{
    public class SearchActionQueryHandler(
        IMapper mapper,
        IRepository<Entity.Action> actionRepository
        ) : SearchQueryHandlerBase<SearchActionQuery, SearchActionFilterDto, SearchActionDto>(mapper)
    {
        protected override async Task<ResponseDto<SearchResultDto<SearchActionDto>>> HandleQuery(SearchActionQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<SearchResultDto<SearchActionDto>>();

            Expression<Func<Entity.Action, bool>> filter = x => true;

            var filters = request.SearchParams?.Filter;

            var actions = await actionRepository.SearchByAsNoTrackingAsync(
                request.SearchParams?.Page?.Page ?? 1,
                request.SearchParams?.Page?.PageSize ?? 10,
                request.SearchParams?.Sort?.GetSortExpressions<Entity.Action>(),
                filter
            );

            var actionDtos = _mapper?.Map<IEnumerable<SearchActionDto>>(actions.Items);

            var searchResult = new SearchResultDto<SearchActionDto>(
                actionDtos ?? [],
                actions.Total,
                request.SearchParams
            );

            response.UpdateData(searchResult);

            return await Task.FromResult(response);
        }
    }
}
