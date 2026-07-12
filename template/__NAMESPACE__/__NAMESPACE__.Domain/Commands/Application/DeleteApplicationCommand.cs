using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.Application
{
    public class DeleteApplicationCommand(Guid id) : CommandBase
    {
        public Guid Id { get; set; } = id;
    }
}
