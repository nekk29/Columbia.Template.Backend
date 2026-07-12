using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.Permission
{
    public class AssignPermissionsCommand(Guid roleId, IEnumerable<Guid> actionIds) : CommandBase
    {
        public Guid RoleId { get; set; } = roleId;
        public IEnumerable<Guid> ActionIds { get; set; } = actionIds;
    }
}
