using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class UpdateUserCommand(UpdateUserDto updateDto) : CommandBase<GetUserDto>
    {
        public UpdateUserDto UpdateDto { get; set; } = updateDto;
    }
}
