using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Extensions;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Extensions;

namespace __NAMESPACE__.Domain.Queries.Application
{
    public class SearchApplicationQueryHandler(
        IMapper mapper,
        IRepository<Entity.Application> applicationRepository
    ) : SearchQueryHandlerBase<SearchApplicationQuery, SearchApplicationFilterDto, SearchApplicationDto>(mapper)
    {
        protected override async Task<ResponseDto<SearchResultDto<SearchApplicationDto>>> HandleQuery(SearchApplicationQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<SearchResultDto<SearchApplicationDto>>();

            Expression<Func<Entity.Application, bool>> filter = x => true;

            var filters = request.SearchParams?.Filter;

            if (!string.IsNullOrEmpty(filters?.Query))
            {
                filter = filter.And(x =>
                    x.Code!.Contains(filters.Query!) ||
                    x.Name!.Contains(filters.Query!)
                );
            }

            var applications = await applicationRepository.SearchByAsNoTrackingAsync(
                request.SearchParams?.Page?.Page ?? 1,
                request.SearchParams?.Page?.PageSize ?? 10,
                request.SearchParams?.Sort?.GetSortExpressions<Entity.Application>(),
                filter
            );

            var applicationDtos = _mapper?.Map<IEnumerable<SearchApplicationDto>>(applications.Items);

            var searchResult = new SearchResultDto<SearchApplicationDto>(
                applicationDtos ?? [],
                applications.Total,
                request.SearchParams
            );

            response.UpdateData(searchResult);

            return await Task.FromResult(response);
        }
    }
}
