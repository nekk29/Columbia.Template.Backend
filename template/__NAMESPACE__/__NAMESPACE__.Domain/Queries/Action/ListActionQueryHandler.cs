using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Queries.Action
{
    public class ListActionQueryHandler(
        IMapper mapper,
        IRepository<Entity.Action> actionRepository
    ) : QueryHandlerBase<ListActionQuery, IEnumerable<ListActionDto>>(mapper)
    {
        protected override async Task<ResponseDto<IEnumerable<ListActionDto>>> HandleQuery(ListActionQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IEnumerable<ListActionDto>>();

            var items = request.ModuleId.HasValue ?
                await actionRepository.FindByAsync(x => x.ModuleId == request.ModuleId.Value) :
                await actionRepository.FindAll().ToListAsync(cancellationToken);

            var itemDtos = _mapper?.Map<IEnumerable<ListActionDto>>(items.OrderBy(x => x.Code));

            response.UpdateData(itemDtos ?? []);

            return await Task.FromResult(response);
        }
    }
}
