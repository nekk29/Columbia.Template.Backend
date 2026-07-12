using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Role;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class UpdateRoleCommand(UpdateRoleDto updateDto) : CommandBase<GetRoleDto>
    {
        public UpdateRoleDto UpdateDto { get; set; } = updateDto;
    }
}
