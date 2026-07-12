using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Application;

namespace __NAMESPACE__.Domain.Commands.Application
{
    public class UpdateApplicationCommand(UpdateApplicationDto updateDto) : CommandBase<GetApplicationDto>
    {
        public UpdateApplicationDto UpdateDto { get; set; } = updateDto;
    }
}
