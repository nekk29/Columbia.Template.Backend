using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class ResetPasswordCommand(ResetPasswordDto resetPasswordDto) : CommandBase
    {
        public ResetPasswordDto ResetPasswordDto { get; set; } = resetPasswordDto;
    }
}
