using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Role;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class CreateRoleCommand(CreateRoleDto createDto) : CommandBase<GetRoleDto>
    {
        public CreateRoleDto CreateDto { get; set; } = createDto;
    }
}
