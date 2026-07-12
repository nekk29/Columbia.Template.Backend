using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class LoginCommand(LoginDto loginDto) : CommandBase<LoginResultDto>
    {
        public LoginDto LoginDto { get; set; } = loginDto;
    }
}
