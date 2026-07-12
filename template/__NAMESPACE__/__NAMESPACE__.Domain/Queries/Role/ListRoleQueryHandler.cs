using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Role;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Queries.Role
{
    public class ListRoleQueryHandler(
        IMapper mapper,
        IRepository<Entity.Application> applicationRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : QueryHandlerBase<ListRoleQuery, IEnumerable<ListRoleDto>>(mapper)
    {
        protected override async Task<ResponseDto<IEnumerable<ListRoleDto>>> HandleQuery(ListRoleQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IEnumerable<ListRoleDto>>();

            Expression<Func<Entity.ApplicationRole, bool>> filter = x => x.IsActive;

            if (request.ApplicationId.HasValue)
                filter = filter.And(x => x.ApplicationId == request.ApplicationId.Value);

            var items = await applicationRoleRepository
                .FindAll()
                .Where(filter)
                .OrderBy(x => x.ApplicationId)
                .ThenBy(x => x.Name)
                .ToListAsync(cancellationToken);

            var applicationIds = items.Select(x => x.ApplicationId).Distinct().ToList();

            var applications = await applicationRepository
                .FindByAsNoTrackingAsync(x => x.IsActive && applicationIds.Contains(x.Id));

            var itemDtos = _mapper?.Map<IEnumerable<ListRoleDto>>(items);

            foreach (var itemDto in itemDtos ?? [])
            {
                var application = applications?.FirstOrDefault(x => x.Id == itemDto.ApplicationId);

                itemDto.ApplicationCode = application?.Code!;
                itemDto.ApplicationName = application?.Name!;
            }

            response.UpdateData(itemDtos ?? []);

            return await Task.FromResult(response);
        }
    }
}
