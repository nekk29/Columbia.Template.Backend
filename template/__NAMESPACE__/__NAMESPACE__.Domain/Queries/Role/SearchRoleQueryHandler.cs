using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Role;
using __NAMESPACE__.Entity.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Extensions;

namespace __NAMESPACE__.Domain.Queries.Role
{
    public class SearchRoleQueryHandler(
        IMapper mapper,
        IRepository<Entity.Application> applicationRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : SearchQueryHandlerBase<SearchRoleQuery, SearchRoleFilterDto, SearchRoleDto>(mapper)
    {
        protected override async Task<ResponseDto<SearchResultDto<SearchRoleDto>>> HandleQuery(SearchRoleQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<SearchResultDto<SearchRoleDto>>();

            Expression<Func<Entity.ApplicationRole, bool>> filter = x => true;

            var filters = request.SearchParams?.Filter;

            if (filters?.ApplicationId.HasValue == true)
                filter = filter.And(x => x.ApplicationId == filters.ApplicationId.Value);

            if (!string.IsNullOrEmpty(filters?.Query))
            {
                filter = filter.And(x =>
                    x.Name!.Contains(filters.Query!) ||
                    x.NormalizedName!.Contains(filters.Query!)
                );
            }

            var sorts = new List<SortExpression<Entity.ApplicationRole>>();

            if (request.SearchParams?.Sort?.Any() == true)
            {
                request.SearchParams.Sort.ToList().ForEach(x =>
                {
                    var sort = IQueryableExtensions.GetSortExpression<Entity.ApplicationRole>(x.Direction, x.Property);
                    if (sort != null) sorts.Add(sort);
                });
            }
            else
            {
                sorts.Add(new SortExpression<Entity.ApplicationRole> { Direction = SortDirection.Asc, Property = x => x.ApplicationId });
                sorts.Add(new SortExpression<Entity.ApplicationRole> { Direction = SortDirection.Asc, Property = x => x.Name! });
            }

            var roles = await applicationRoleRepository.SearchByAsNoTrackingAsync(
                request.SearchParams?.Page?.Page ?? 1,
                request.SearchParams?.Page?.PageSize ?? 10,
                sorts,
                filter
            );

            var roleDtos = _mapper?.Map<IEnumerable<SearchRoleDto>>(roles.Items);
            var applicationIds = roles.Items.Select(x => x.ApplicationId).Distinct().ToList();

            var applications = await applicationRepository
               .FindByAsNoTrackingAsync(x => x.IsActive && applicationIds.Contains(x.Id));

            foreach (var roleDto in roleDtos ?? [])
            {
                var application = applications?.FirstOrDefault(x => x.Id == roleDto.ApplicationId);

                roleDto.ApplicationCode = application?.Code!;
                roleDto.ApplicationName = application?.Name!;
            }

            var searchResult = new SearchResultDto<SearchRoleDto>(
                roleDtos ?? [],
                roles.Total,
                request.SearchParams
            );

            response.UpdateData(searchResult);

            return await Task.FromResult(response);
        }
    }
}
