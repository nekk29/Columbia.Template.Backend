using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Queries.Application
{
    public class ListApplicationQueryHandler(
        IMapper mapper,
        IRepository<Entity.Application> applicationRepository
    ) : QueryHandlerBase<ListApplicationQuery, IEnumerable<ListApplicationDto>>(mapper)
    {
        protected override async Task<ResponseDto<IEnumerable<ListApplicationDto>>> HandleQuery(ListApplicationQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IEnumerable<ListApplicationDto>>();
            var items = await applicationRepository.FindAll().ToListAsync(cancellationToken);
            var itemDtos = _mapper?.Map<IEnumerable<ListApplicationDto>>(items);

            response.UpdateData(itemDtos ?? []);

            return await Task.FromResult(response);
        }
    }
}
