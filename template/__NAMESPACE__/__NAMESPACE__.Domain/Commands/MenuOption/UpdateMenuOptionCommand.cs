using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.MenuOption;

namespace __NAMESPACE__.Domain.Commands.MenuOption
{
    public class UpdateMenuOptionCommand(UpdateMenuOptionDto updateDto) : CommandBase<GetMenuOptionDto>
    {
        public UpdateMenuOptionDto UpdateDto { get; set; } = updateDto;
    }
}
