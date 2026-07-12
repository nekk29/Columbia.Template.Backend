using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class CreateUserCommand(CreateUserDto createDto) : CommandBase<GetUserDto>
    {
        public CreateUserDto CreateDto { get; set; } = createDto;
    }
}
