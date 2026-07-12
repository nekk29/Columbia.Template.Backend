using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class DeleteUserCommand(Guid id) : CommandBase
    {
        public Guid Id { get; set; } = id;
    }
}
