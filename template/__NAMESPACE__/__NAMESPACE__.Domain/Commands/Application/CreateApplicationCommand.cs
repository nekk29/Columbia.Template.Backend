using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Application;

namespace __NAMESPACE__.Domain.Commands.Application
{
    public class CreateApplicationCommand(CreateApplicationDto createDto) : CommandBase<GetApplicationDto>
    {
        public CreateApplicationDto CreateDto { get; set; } = createDto;
    }
}
