using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Repository.Abstractions.Base;
using Microsoft.AspNetCore.Identity;

namespace __NAMESPACE__.Domain.Queries.User
{
    public class GetUserQueryHandler(
        IMapper mapper,
        UserManager<Entity.ApplicationUser> userManager,
        IRepository<Entity.ApplicationUser> applicationUserRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : QueryHandlerBase<GetUserQuery, GetUserDto>(mapper)
    {
        protected override async Task<ResponseDto<GetUserDto>> HandleQuery(GetUserQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetUserDto>();

            var user = await applicationUserRepository.GetByAsync(x => x.Id == request.Id);
            var rolesNames = user != null ? await userManager.GetRolesAsync(user) : [];

            var roles = await applicationRoleRepository
                .FindByAsNoTrackingAsync(x => rolesNames.Contains(x.Name!) && x.IsActive);

            var userDto = _mapper?.Map<GetUserDto>(user);

            if (userDto != null)
            {
                userDto.RoleIds = roles.Select(x => x.Id) ?? [];
                response.UpdateData(userDto);
            }

            return await Task.FromResult(response);
        }
    }
}
