using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Module;

namespace __NAMESPACE__.Domain.Commands.Module
{
    public class UpdateModuleCommand(UpdateModuleDto updateDto) : CommandBase<GetModuleDto>
    {
        public UpdateModuleDto UpdateDto { get; set; } = updateDto;
    }
}
