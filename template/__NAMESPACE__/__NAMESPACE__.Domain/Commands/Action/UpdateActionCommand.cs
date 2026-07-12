using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Action;

namespace __NAMESPACE__.Domain.Commands.Action
{
    public class UpdateActionCommand(UpdateActionDto updateDto) : CommandBase<GetActionDto>
    {
        public UpdateActionDto UpdateDto { get; set; } = updateDto;
    }
}
