using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Role;
using __NAMESPACE__.Repository.Abstractions.Base;

namespace __NAMESPACE__.Domain.Queries.Role
{
    public class GetRoleQueryHandler(
        IMapper mapper,
        IRepository<Entity.Application> applicationRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : QueryHandlerBase<GetRoleQuery, GetRoleDto>(mapper)
    {
        protected override async Task<ResponseDto<GetRoleDto>> HandleQuery(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetRoleDto>();

            var role = await applicationRoleRepository.GetByAsync(x => x.Id == request.Id);
            var roleDto = _mapper?.Map<GetRoleDto>(role);

            if (roleDto != null)
            {
                var application = await applicationRepository.GetByAsNoTrackingAsync(x => x.Id == roleDto.ApplicationId);

                roleDto.ApplicationCode = application?.Code!;
                roleDto.ApplicationName = application?.Name!;
            }

            response.UpdateData(roleDto!);

            return await Task.FromResult(response);
        }
    }
}
