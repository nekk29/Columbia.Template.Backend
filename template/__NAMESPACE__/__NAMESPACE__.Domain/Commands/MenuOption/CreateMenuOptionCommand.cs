using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.MenuOption;

namespace __NAMESPACE__.Domain.Commands.MenuOption
{
    public class CreateMenuOptionCommand(CreateMenuOptionDto createDto) : CommandBase<GetMenuOptionDto>
    {
        public CreateMenuOptionDto CreateDto { get; set; } = createDto;
    }
}
