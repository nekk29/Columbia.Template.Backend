using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.Module
{
    public class DeleteModuleCommand(Guid id) : CommandBase
    {
        public Guid Id { get; set; } = id;
    }
}
