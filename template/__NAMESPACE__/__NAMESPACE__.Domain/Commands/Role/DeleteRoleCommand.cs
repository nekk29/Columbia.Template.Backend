using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class DeleteRoleCommand(Guid id) : CommandBase
    {
        public Guid Id { get; set; } = id;
    }
}
