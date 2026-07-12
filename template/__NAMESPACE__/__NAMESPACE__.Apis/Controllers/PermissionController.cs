using __NAMESPACE__.Apis.Controllers.Base;
using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Permission;
using Microsoft.AspNetCore.Mvc;

namespace __NAMESPACE__.Apis.Controllers
{
    [ApiController]
    [Security.Authorize]
    [Route("api/permission")]
    public class PermissionController(
        IServiceProvider serviceProvider,
        IPermissionApplication permissionApplication
    ) : ApiControllerBase(serviceProvider)
    {
        [HttpPost]
        [Route("{roleId}/assign")]
        public async Task<ResponseDto> AssignRolePermissions(Guid roleId, [FromBody] IEnumerable<Guid> actionIds)
            => await permissionApplication.AssignPermissions(roleId, actionIds);

        [HttpGet("{roleId}/role-permissions")]
        public async Task<ResponseDto<IEnumerable<ListRolePermissionDto>>> ListRole(Guid roleId)
            => await permissionApplication.ListRole(roleId);

        [HttpGet("{applicationCode}/user-permissions")]
        public async Task<ResponseDto<IEnumerable<ListPermissionDto>>> ListUser(string applicationCode)
            => await permissionApplication.ListUser(applicationCode);
    }
}
