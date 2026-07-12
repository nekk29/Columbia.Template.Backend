using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.Action
{
    public class DeleteActionCommand(Guid id) : CommandBase
    {
        public Guid Id { get; set; } = id;
    }
}
