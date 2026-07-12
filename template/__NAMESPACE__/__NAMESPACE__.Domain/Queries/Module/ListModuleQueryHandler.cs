using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Module;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Queries.Module
{
    public class ListModuleQueryHandler(
        IMapper mapper,
        IRepository<Entity.Action> actionRepository,
        IRepository<Entity.Module> moduleRepository
    ) : QueryHandlerBase<ListModuleQuery, IEnumerable<ListModuleDto>>(mapper)
    {
        protected override async Task<ResponseDto<IEnumerable<ListModuleDto>>> HandleQuery(ListModuleQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IEnumerable<ListModuleDto>>();

            Expression<Func<Entity.Module, bool>> filter = x => true;

            if (request.ApplicationId.HasValue)
                filter = filter.And(x => x.ApplicationId == request.ApplicationId.Value);

            var items = await moduleRepository
                .FindAll()
                .Where(filter)
                .OrderBy(x => x.Application.Name)
                .ThenBy(x => x.Name)
                .Include(x => x.Application)
                .ToListAsync(cancellationToken);

            var itemDtos = _mapper?.Map<IEnumerable<ListModuleDto>>(items);

            if (request.IncludeActions)
            {
                var moduleIds = items.Select(x => x.Id);
                var actions = await actionRepository
                    .FindAll()
                    .Where(x => moduleIds.Contains(x.ModuleId))
                    .OrderBy(x => x.Code)
                    .ToListAsync(cancellationToken);

                foreach (var itemDto in itemDtos ?? [])
                {
                    var itemActions = actions.Where(x => x.ModuleId == itemDto.Id);
                    var itemActionDtos = _mapper?.Map<IEnumerable<GetActionDto>>(itemActions) ?? [];
                    itemDto.Actions = itemActionDtos;
                }
            }

            response.UpdateData(itemDtos ?? []);

            return await Task.FromResult(response);
        }
    }
}
