using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class ForgotPasswordCommand(ForgotPasswordDto forgotPasswordDto) : CommandBase
    {
        public ForgotPasswordDto ForgotPasswordDto { get; set; } = forgotPasswordDto;
    }
}
