using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.MenuOption;
using __NAMESPACE__.Repository.Abstractions.Base;

namespace __NAMESPACE__.Domain.Queries.MenuOption
{
    public class GetMenuOptionQueryHandler(
        IMapper mapper,
        IRepository<Entity.MenuOption> menuOptionRepository
    ) : QueryHandlerBase<GetMenuOptionQuery, GetMenuOptionDto>(mapper)
    {
        protected override async Task<ResponseDto<GetMenuOptionDto>> HandleQuery(GetMenuOptionQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetMenuOptionDto>();

            var menuOption = await menuOptionRepository.GetByAsync(
                x => x.Id == request.Id,
                x => x.Application,
                x => x.ParentMenuOption!,
                x => x.Action.Module
            );

            var menuOptionDto = _mapper?.Map<GetMenuOptionDto>(menuOption);

            if (menuOptionDto != null)
                response.UpdateData(menuOptionDto);

            return await Task.FromResult(response);
        }
    }
}
