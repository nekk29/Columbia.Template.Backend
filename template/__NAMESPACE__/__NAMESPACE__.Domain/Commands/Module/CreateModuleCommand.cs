using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Module;

namespace __NAMESPACE__.Domain.Commands.Module
{
    public class CreateModuleCommand(CreateModuleDto createDto) : CommandBase<GetModuleDto>
    {
        public CreateModuleDto CreateDto { get; set; } = createDto;
    }
}
