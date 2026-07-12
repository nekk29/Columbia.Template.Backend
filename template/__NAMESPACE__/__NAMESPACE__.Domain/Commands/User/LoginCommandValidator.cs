using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class LoginCommandValidator : CommandValidatorBase<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RequiredInformation(x => x.LoginDto).DependentRules(() =>
            {
                RequiredField(x => x.LoginDto.ApplicationCode, Resources.User.NoAccessToApplication)
                    .DependentRules(() =>
                    {
                        RequiredString(x => x.LoginDto.UserName, Resources.User.UserName, default, 256);
                    });
                RequiredString(x => x.LoginDto.Password, Resources.User.Password, default, 256);
            });
        }
    }
}
