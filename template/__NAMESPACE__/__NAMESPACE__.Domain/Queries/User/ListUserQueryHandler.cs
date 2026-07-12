using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Queries.User
{
    public class ListUserQueryHandler(
        IMapper mapper,
        IRepository<Entity.ApplicationUser> applicationUserRepository
    ) : QueryHandlerBase<ListUserQuery, IEnumerable<ListUserDto>>(mapper)
    {
        protected override async Task<ResponseDto<IEnumerable<ListUserDto>>> HandleQuery(ListUserQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IEnumerable<ListUserDto>>();
            var items = await applicationUserRepository.FindAll().Where(x => x.IsActive).ToListAsync(cancellationToken);
            var itemDtos = _mapper?.Map<IEnumerable<ListUserDto>>(items);

            response.UpdateData(itemDtos ?? []);

            return await Task.FromResult(response);
        }
    }
}
