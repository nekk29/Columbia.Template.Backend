using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;

namespace __NAMESPACE__.Domain.Queries.Action
{
    public class GetActionQueryHandler(
        IMapper mapper,
        IRepository<Entity.Action> actionRepository
    ) : QueryHandlerBase<GetActionQuery, GetActionDto>(mapper)
    {
        protected override async Task<ResponseDto<GetActionDto>> HandleQuery(GetActionQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetActionDto>();
            var action = await actionRepository.GetByAsync(x => x.Id == request.Id);
            var actionDto = _mapper?.Map<GetActionDto>(action);

            if (actionDto != null)
                response.UpdateData(actionDto);

            return await Task.FromResult(response);
        }
    }
}
