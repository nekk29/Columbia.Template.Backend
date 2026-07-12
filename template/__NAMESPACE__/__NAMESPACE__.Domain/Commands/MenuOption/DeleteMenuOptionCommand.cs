using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.MenuOption
{
    public class DeleteMenuOptionCommand(Guid id) : CommandBase
    {
        public Guid Id { get; set; } = id;
    }
}
