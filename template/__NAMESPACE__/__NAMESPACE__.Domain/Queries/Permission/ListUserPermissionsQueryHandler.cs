using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Permission;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Domain.Queries.Permission
{
    public class ListUserPermissionsQueryHandler(
        IMapper mapper,
        IUserIdentity userIdentity,
        UserManager<Entity.ApplicationUser> userManager,
        IRepository<Entity.Permission> permissionRepository,
        IRepository<Entity.Application> applicationRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : QueryHandlerBase<ListUserPermissionsQuery, IEnumerable<ListPermissionDto>>(mapper)
    {
        protected override async Task<ResponseDto<IEnumerable<ListPermissionDto>>> HandleQuery(ListUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IEnumerable<ListPermissionDto>>();

            var application = await applicationRepository.GetByAsNoTrackingAsync(x => x.IsActive && x.Code == request.ApplicationCode);
            if (application == null)
            {
                response.UpdateData([]);
                return response;
            }

            var user = await userManager.FindByNameAsync(userIdentity.GetUserName());
            if (user == null)
            {
                response.UpdateData([]);
                return response;
            }

            var rolesNames = await userManager.GetRolesAsync(user);

            var roles = await applicationRoleRepository
                .FindAllAsNoTracking()
                .Where(x => x.ApplicationId == application.Id && rolesNames.Contains(x.Name!) && x.IsActive)
                .ToListAsync(cancellationToken) ?? [];

            var roleIds = roles.Select(x => x.Id).ToList();
            
            var permissions = await permissionRepository.FindByAsNoTrackingAsync(
                x => x.IsActive && x.Action.IsActive && roleIds.Contains(x.RoleId),
                x => x.Action.Module
            );

            var permissionDtos = _mapper!.Map<IEnumerable<ListPermissionDto>>(permissions);
            
            foreach (var permissionDto in permissionDtos)
            {
                var role = roles.FirstOrDefault(x => x.Id == permissionDto.RoleId);
                
                permissionDto.RoleName = role?.Name!;
                permissionDto.IsAssigned = true;
            }
            
            response.UpdateData(permissionDtos);

            return response;
        }
    }
}
