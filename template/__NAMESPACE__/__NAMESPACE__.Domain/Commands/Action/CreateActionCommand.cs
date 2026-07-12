using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Action;

namespace __NAMESPACE__.Domain.Commands.Action
{
    public class CreateActionCommand(CreateActionDto createDto) : CommandBase<GetActionDto>
    {
        public CreateActionDto CreateDto { get; set; } = createDto;
    }
}
