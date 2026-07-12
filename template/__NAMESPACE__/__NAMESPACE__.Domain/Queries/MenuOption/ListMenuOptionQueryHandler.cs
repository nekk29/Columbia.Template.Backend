using System.Linq.Expressions;
using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Domain.Queries.Permission;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.MenuOption;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Queries.MenuOption
{
    public class ListMenuOptionQueryHandler(
        IMapper mapper,
        IMediator mediator,
        IRepository<Entity.MenuOption> menuOptionRepository
    ) : QueryHandlerBase<ListMenuOptionQuery, IEnumerable<ListMenuOptionDto>>(mapper, mediator)
    {
        protected override async Task<ResponseDto<IEnumerable<ListMenuOptionDto>>> HandleQuery(ListMenuOptionQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IEnumerable<ListMenuOptionDto>>();

            Expression<Func<Entity.MenuOption, bool>> filter = x => x.Application.IsActive && x.Application.Code == request.ApplicationCode;

            if (!request.IncludeAll)
            {
                var permissionsResponse = await _mediator!.Send(new ListUserPermissionsQuery(request.ApplicationCode), cancellationToken);
                var actionIds = permissionsResponse.Data?.Select(x => x.ActionId) ?? [];
                filter.And(x => x.IsActive && actionIds.Contains(x.Action.Id));
            }

            var menuOptions = await menuOptionRepository.FindByAsNoTrackingAsync(
                filter,
                x => x.Application,
                x => x.ParentMenuOption!,
                x => x.Action.Module
            );

            var listMenuOptionDtos = _mapper!.Map<IEnumerable<ListMenuOptionDto>>(menuOptions);

            listMenuOptionDtos = listMenuOptionDtos.OrderBy(x => x.SortOrder);

            response.UpdateData(listMenuOptionDtos);

            return response;
        }
    }
}
